using Microsoft.Playwright.NUnit;
using SauceDemo.SauceDemoPages;

namespace SauceDemo.SauceDemoTests
{
    public class TestSetup : PageTest
    {
        public LoginPage LoginPage { get; set; }
        public InventoryPage InventoryPage { get; set; }
        public ProductPage ProductPage { get; set; }
        public CartPage CartPage { get; set; }

        [SetUp]
        public void Start()
        {
            LoginPage = new LoginPage(Page);
            InventoryPage = new InventoryPage(Page);
            ProductPage = new ProductPage(Page);
            CartPage = new CartPage(Page);
        }

        [TearDown]
        public async Task Stop()
        {
            await Page.CloseAsync();
        }
    }
}
