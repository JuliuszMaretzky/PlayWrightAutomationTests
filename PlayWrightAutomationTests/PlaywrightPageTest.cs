//https://www.youtube.com/watch?v=d31_UcGIac8&list=PLyR3u3h9srduMQ0G2SefuMz3Dkkkzs59k&index=2

using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlayWrightAutomationTests
{
    [TestFixture]
    public class PlaywrightPageTest : PageTest
    {
        [Test]
        public async Task Login_PageTest_TC001()
        {
            await Page.GotoAsync("https://adactinhotelapp.com/");
            await Expect(Page).ToHaveTitleAsync(new Regex("Adactin.com - Hotel Reservation System"));
            await Page.FillAsync("#username", "Pepeusz123");
            await Page.FillAsync("#password", "pepe123_P");
            await Page.ClickAsync("#login");
            await Expect(Page).ToHaveTitleAsync(new Regex("Adactin.com - Search Hotel"));

            var locator = Page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(new Regex("Welcome to Adactin Group of Hotels"));

            await Page.CloseAsync();
        }

        [Test]
        public async Task GetByPlaceHolder()
        {
            await Page.GotoAsync("https://demoqa.com/text-box");
            await Page.GetByPlaceholder("Full Name").FillAsync("Tester1");
            await Page.GetByPlaceholder("name@example.com").FillAsync("pepe@pepe.pepe");
            await Page.GetByPlaceholder("Current Address").FillAsync("Pepe 12/3");
        }

        [Test]
        public async Task GetByText()
        {
            await Page.GotoAsync("https://demoqa.com/links");
            await Page.GetByText("Created").ClickAsync();
        }

        [Test]
        public async Task GetByAltText()
        {
            await Page.GotoAsync("https://playwright.dev/dotnet/docs/locators#locate-by-alt-text");
            await Page.GetByAltText("playwright logo").First.ClickAsync();
        }

        [Test]
        public async Task GetByLabel()
        {
            await Page.GotoAsync("https://playwright.dev/dotnet/docs/locators#locate-by-title");
            await Page.GetByLabel("Password").FillAsync("secret112233");
            Thread.Sleep(5000);
        }

        [Test]
        public async Task GetByTitle()
        {
            await Page.GotoAsync("https://playwright.dev/dotnet/docs/locators#locate-by-title");
            await Expect(Page.GetByTitle("Issues count")).ToHaveTextAsync("25 issues");
        }

        [Test]
        public async Task GetByRole()
        {
            await Page.GotoAsync("https://playwright.dev/dotnet/docs/locators#locate-by-role");
            await Page.SetViewportSizeAsync(width: 1920, height: 1080);

            await Expect(Page
                .GetByRole(AriaRole.Heading, new() { Name = "Sign up"}))
                .ToBeVisibleAsync();

            await Page
                .GetByRole(AriaRole.Checkbox, new() { Name = "Subscribe" })
                .CheckAsync();

            await Page
                .GetByRole(AriaRole.Button, new()
                {
                    NameRegex = new Regex("submit", RegexOptions.IgnoreCase)
                })
                .ClickAsync();
        }
    }
}
