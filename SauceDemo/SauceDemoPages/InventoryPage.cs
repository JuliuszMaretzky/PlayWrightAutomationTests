using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoPages
{
    public class InventoryPage
    {
        private const string pageAddress = "https://www.saucedemo.com/inventory.html";
        private readonly IPage _page;
        private readonly ILocator pageHeader;
        private readonly ILocator productSortDropDown;
        private readonly ILocator activeSortOption;

        public readonly string productNameXPath;
        public readonly Dictionary<string, string> productSortOptions;

        public InventoryPage(IPage page)
        {
            _page = page;
            pageHeader = _page.Locator("//*[@class='app_logo']");
            productSortDropDown = _page.Locator("//*[@class='product_sort_container']");
            activeSortOption = _page.Locator("//*[@class='active_option']");

            productNameXPath = "//*[contains(@class,'inventory_item_name')]";
            productSortOptions = new Dictionary<string, string>() 
            {
                ["Name (A to Z)"] = "az",
                ["Name (Z to A)"] = "za",
                ["Price (low to high)"] = "lohi",
                ["Price (high to low)"] = "hilo"
            };
        }

        public async Task WaitForLoad()
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
