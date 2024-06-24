using Microsoft.Playwright.NUnit;
using SauceDemo.SauceDemoPages;

namespace SauceDemo.SauceDemoTests
{
    public class TestSetup : PageTest
    {
        public LoginPage LoginPage { get; set; }
        public InventoryPage InventoryPage { get; set; }

        [SetUp]
        public void Start()
        {
            LoginPage = new LoginPage(Page);
            InventoryPage = new InventoryPage(Page);
        }

        [TearDown]
        public async Task Stop()
        {
            await Page.CloseAsync();
        }
    }
}
