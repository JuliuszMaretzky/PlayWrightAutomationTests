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

        [SetUp]
        public async Task SetUp()
        {
            await LoginPage.GoToLoginPage();
            await LoginPage.FillCredentials(standardUsername, standardPassword);
            await LoginPage.ClickLogin();
            await InventoryPage.WaitForLoad();
        }

        private async Task AssertProductNamesOrder(List<string> productNames)
        {
            for (int i = 0; i < productNames.Count; i++)
            {
                await Assertions.Expect(InventoryPage.GetElementWithIndex(InventoryPage.productNameXPath, i+1)).ToHaveTextAsync(productNames[i]);
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
            await AssertProductNamesOrder(productNamesList);
            await InventoryPage.SortProducts(InventoryPage.productSortOptions["Name (Z to A)"]);
            productNamesList.Reverse();
            await AssertProductNamesOrder(productNamesList);
        }

        [Test]
        public async Task Check_If_Products_Have_Same_Parameters_On_List_And_In_Details()
        {
            var numberOfItemsOnList = 6;
            var values = new Dictionary<string, string>();
            ILocator product;
            for (int i = 1; i <= numberOfItemsOnList; i++)
            {
                product = InventoryPage.GetElementWithIndex(InventoryPage.productNameXPath, i);
                values["Name"] = await product.TextContentAsync();
                values["Description"] = await InventoryPage.GetElementWithIndex(InventoryPage.productDescriptionXPath, i).TextContentAsync();
                values["Price"] = await InventoryPage.GetElementWithIndex(InventoryPage.productPriceXPath, i).TextContentAsync();
                await product.ClickAsync();
                await ProductPage.WaitForLoad();
                await Assertions.Expect(ProductPage.productNameText).ToHaveTextAsync(values["Name"]);
                await Assertions.Expect(ProductPage.productDescriptionText).ToHaveTextAsync(values["Description"]);
                await Assertions.Expect(ProductPage.productPriceText).ToHaveTextAsync(values["Price"]);
                await ProductPage.BackToInventory();
            }
        }

        [Test]
        [TestCase("Inventory Page")]
        [TestCase("Cart Page")]
        public async Task Add_Item_To_Cart_And_Remove(string removeFrom)
        {
            await Assertions.Expect(InventoryPage.GetElementWithIndex(InventoryPage.addToCartXPath, 1)).ToHaveTextAsync("Add to cart");
            await Assertions.Expect(InventoryPage.cartItemCounter).Not.ToBeVisibleAsync();
            var productName = await InventoryPage.GetElementWithIndex(InventoryPage.productNameXPath, 1).TextContentAsync();
            var productDescription = await InventoryPage.GetElementWithIndex(InventoryPage.productDescriptionXPath, 1).TextContentAsync();
            var productPrice = await InventoryPage.GetElementWithIndex(InventoryPage.productPriceXPath, 1).TextContentAsync();
            await InventoryPage.GetElementWithIndex(InventoryPage.addToCartXPath, 1).ClickAsync();
            await Assertions.Expect(InventoryPage.GetElementWithIndex(InventoryPage.addToCartXPath, 1)).ToHaveTextAsync("Remove");
            await Assertions.Expect(InventoryPage.cartItemCounter).ToHaveTextAsync("1");
            if (removeFrom == "Inventory Page")
            {
                await InventoryPage.GetElementWithIndex(InventoryPage.addToCartXPath, 1).ClickAsync();
                await Assertions.Expect(InventoryPage.GetElementWithIndex(InventoryPage.addToCartXPath, 1)).ToHaveTextAsync("Add to cart");
                await Assertions.Expect(InventoryPage.cartItemCounter).Not.ToBeVisibleAsync();
            }
            await InventoryPage.cartIcon.ClickAsync();
            await CartPage.WaitForLoad();
            if (removeFrom == "Cart Page")
            {
                await Assertions.Expect(InventoryPage.cartItemCounter).ToHaveTextAsync("1");
                await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.productNameXPath, 1)).ToHaveTextAsync(productName);
                await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.productDescriptionXPath, 1)).ToHaveTextAsync(productDescription);
                await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.productPriceXPath, 1)).ToHaveTextAsync(productPrice);
                await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.removeProductButtonXPath, 1)).ToBeVisibleAsync();
                await CartPage.GetElementWithIndex(CartPage.removeProductButtonXPath, 1).ClickAsync();
            }
            await Assertions.Expect(InventoryPage.cartItemCounter).Not.ToBeVisibleAsync();
            await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.productNameXPath, 1)).Not.ToBeVisibleAsync();
            await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.productDescriptionXPath, 1)).Not.ToBeVisibleAsync();
            await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.productPriceXPath, 1)).Not.ToBeVisibleAsync();
            await Assertions.Expect(CartPage.GetElementWithIndex(CartPage.removeProductButtonXPath, 1)).Not.ToBeVisibleAsync();
            await CartPage.continueShoppingButton.ClickAsync();
            await InventoryPage.WaitForLoad();
            await Assertions.Expect(InventoryPage.cartItemCounter).Not.ToBeVisibleAsync();
        }
    }
}
