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

        [Test]
        public async Task Check_If_Product_Sorting_Works_Properly()
        {
            await LoginPage.GoToLoginPage();
            await LoginPage.FillCredentials(standardUsername, standardPassword);
            await LoginPage.ClickLogin();
            await InventoryPage.WaitForLoad();
            await InventoryPage.SortProducts(InventoryPage.productSortOptions["Name (Z to A)"]);
        }
    }
}
