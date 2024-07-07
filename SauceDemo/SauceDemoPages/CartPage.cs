using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoPages
{
    public class CartPage:BasePage
    {
        public readonly ILocator continueShoppingButton;
        public readonly string productNameXPath = "//*[contains(@class,'inventory_item_name')]";
        public readonly string productDescriptionXPath = "//*[@class='inventory_item_desc']";
        public readonly string productPriceXPath = "//*[@class='inventory_item_price']";
        public readonly string removeProductButtonXPath = "//button[text()='Remove']";
        public readonly string cartQuantityTextXPath = "//*[@class='cart_quantity']";

        private ILocator pageTitleLabel;

        public CartPage(IPage page) : base(page) 
        {
            pageTitleLabel = _page.Locator("//*[@class='title' and text()='Your Cart']");
            continueShoppingButton = _page.Locator("//button[text()='Continue Shopping']");
        }

        public override async Task WaitForLoad()
        {
            await Assertions.Expect(pageTitleLabel).ToBeVisibleAsync();
        }
    }
}
