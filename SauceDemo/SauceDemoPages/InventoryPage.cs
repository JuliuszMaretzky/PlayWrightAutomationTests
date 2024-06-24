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


        public InventoryPage(IPage page)
        {
            _page = page;
            pageHeader = _page.Locator("//*[@class='app_logo']");
        }

        public async Task WaitForLoad()
        {
            await Assertions.Expect(pageHeader).ToHaveTextAsync("Swag Labs");
        }
    }
}
