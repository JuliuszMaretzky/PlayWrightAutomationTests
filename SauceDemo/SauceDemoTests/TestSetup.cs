using Microsoft.Playwright.NUnit;
using SauceDemo.SauceDemoPages;

namespace SauceDemo.SauceDemoTests
{
    public class TestSetup : PageTest
    {
        public LoginPage LoginPage { get; set; }

        [SetUp]
        public void Start()
        {
            LoginPage = new LoginPage(Page);
        }

        [TearDown]
        public async Task Stop()
        {
            await Page.CloseAsync();
        }
    }
}
