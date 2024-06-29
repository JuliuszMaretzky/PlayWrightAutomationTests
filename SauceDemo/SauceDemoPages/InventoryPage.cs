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
        private readonly ILocator pageHeader;
        private readonly ILocator productSortDropDown;
        private readonly ILocator activeSortOption;

        public readonly string productNameXPath;
        public readonly string productDescriptionXPath;
        public readonly string productPriceXPath;
        public readonly Dictionary<string, string> productSortOptions;

        public InventoryPage(IPage page):base(page)
        {
            pageHeader = _page.Locator("//*[@class='app_logo']");
            productSortDropDown = _page.Locator("//*[@class='product_sort_container']");
            activeSortOption = _page.Locator("//*[@class='active_option']");
            
            productNameXPath = "//*[contains(@class,'inventory_item_name')]";
            productDescriptionXPath = "//*[@class='inventory_item_desc']"; 
            productPriceXPath = "//*[@class='inventory_item_price']"; 
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

        public ILocator GetElementWithIndex(string itemXPath, int index)
        {
            return _page.Locator($"({itemXPath})[{index}]");
        }
    }
}
