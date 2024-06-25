using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoTests
{
    public class InventoryTests : TestSetup
    {
        private const string standardPassword = "secret_sauce";
        private const string standardUsername = "standard_user";

        private async Task AssertProductNamesOrder(List<string> productNames)
        {
            for (int i = 0; i < productNames.Count; i++)
            {
                await Assertions.Expect(Page.Locator($"({InventoryPage.productNameXPath})[{i + 1}]")).ToHaveTextAsync(productNames[i]);
            }
        }

        [Test]
        public async Task Check_If_Product_Sorting_From_Z_To_A_Works_Properly()
        {
            var productNamesList = new List<string>() { 
                "Sauce Labs Backpack",
                "Sauce Labs Bike Light",
                "Sauce Labs Bolt T-Shirt",
                "Sauce Labs Fleece Jacket",
                "Sauce Labs Onesie",
                "Test.allTheThings() T-Shirt (Red)"
            };
            await LoginPage.GoToLoginPage();
            await LoginPage.FillCredentials(standardUsername, standardPassword);
            await LoginPage.ClickLogin();
            await InventoryPage.WaitForLoad();
            await AssertProductNamesOrder(productNamesList);
            await InventoryPage.SortProducts(InventoryPage.productSortOptions["Name (Z to A)"]);
            productNamesList.Reverse();
            await AssertProductNamesOrder(productNamesList);
        }
    }
}
