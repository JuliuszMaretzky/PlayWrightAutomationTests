using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoPages
{
    public class ProductPage : BasePage
    {
        private readonly ILocator backToProductsButton;
        public readonly ILocator productNameText;
        public readonly ILocator productDescriptionText;
        public readonly ILocator productPriceText;

        public ProductPage(IPage Page) : base(Page)
        {
            backToProductsButton = _page.Locator("//button[@id='back-to-products']");
            productNameText = _page.Locator("//*[@class='inventory_details_name large_size']");
            productDescriptionText = _page.Locator("//*[@class='inventory_details_desc large_size']");
            productPriceText = _page.Locator("//*[@class='inventory_details_price']");
        }

        public async Task WaitForLoad()
        {
            await Assertions.Expect(backToProductsButton).ToBeVisibleAsync();
        }

        public async Task BackToInventory()
        {
            await backToProductsButton.ClickAsync();
            await new InventoryPage(_page).WaitForLoad();
        }
    }
}
