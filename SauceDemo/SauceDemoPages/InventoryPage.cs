using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoPages
{
    public class InventoryPage : BasePage
    {
        public readonly ILocator cartIcon;
        public readonly ILocator cartItemCounter;
        public readonly string productNameXPath = "//*[contains(@class,'inventory_item_name')]";
        public readonly string productDescriptionXPath = "//*[@class='inventory_item_desc']";
        public readonly string productPriceXPath = "//*[@class='inventory_item_price']";
        public readonly string addToCartXPath = "//button[contains(@class,'btn_inventory')]";
        public readonly Dictionary<string, string> productSortOptions = new Dictionary<string, string>()
        {
            ["Name (A to Z)"] = "az",
            ["Name (Z to A)"] = "za",
            ["Price (low to high)"] = "lohi",
            ["Price (high to low)"] = "hilo"
        };

        private readonly ILocator pageHeader;
        private readonly ILocator productSortDropDown;
        private readonly ILocator activeSortOption;

        public InventoryPage(IPage page):base(page)
        {
            pageHeader = _page.Locator("//*[@class='app_logo']");
            productSortDropDown = _page.Locator("//*[@class='product_sort_container']");
            activeSortOption = _page.Locator("//*[@class='active_option']");
            cartIcon = _page.Locator("//a[@class='shopping_cart_link']");
            cartItemCounter = _page.Locator("//*[@class='shopping_cart_badge']");
        }

        public override async Task WaitForLoad()
        {
            await Assertions.Expect(pageHeader).ToHaveTextAsync("Swag Labs");
            await Assertions.Expect(activeSortOption).ToHaveTextAsync("Name (A to Z)");
        }

        public async Task SortProducts(string sortBy)
        {
            await productSortDropDown.ClickAsync();
            await productSortDropDown.SelectOptionAsync(new[] { sortBy });
        }
    }
}
