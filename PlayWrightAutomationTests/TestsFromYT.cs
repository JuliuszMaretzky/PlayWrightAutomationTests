using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace PlayWrightAutomationTests
{
    public class TestsFromYT
    {
        [Test]
        public async Task Login_TC001()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions 
                { 
                    Headless = false, 
                    SlowMo = 50, 
                    Timeout = 80000 
                });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://adactinhotelapp.com");
            await page.FillAsync("#username", "Pepeusz123");
            await page.FillAsync("#password", "pepe123_P");
            await page.ClickAsync("#login");
            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.CloseAsync();
        }

        [Test]
        public async Task Login_TC002()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 50,
                    Timeout = 80000
                });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://adactinhotelapp.com");
            await page.Locator("#username").FillAsync("Pepeusz123");
            await page.Locator("#password").FillAsync("pepe123_P");
            await page.Locator("#login").ClickAsync();
            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.CloseAsync();
        }

        [Test]
        public async Task Login_TC003()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Firefox.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    //Channel = "msedge",
                    SlowMo = 500,
                    Timeout = 80000
                });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://adactinhotelapp.com");
            await page.Locator("#username").FillAsync("Pepeusz123");
            await page.Locator("#password").FillAsync("pepe123_P");
            await page.Locator("#login").ClickAsync();
            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.CloseAsync();
        }

        [Test]
        public async Task Login_Video_TC004()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 50,
                    Timeout = 1200000
                });
            var context = await browser.NewContextAsync(new()
                {
                    RecordVideoDir = "video/",
                    RecordVideoSize = new RecordVideoSize() { Width = 1920, Height = 1080 },
                    ViewportSize = new ViewportSize() { Width = 1920, Height = 1080 }
                });
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://adactinhotelapp.com");
            await page.Locator("#username").FillAsync("Pepeusz123");
            await page.Locator("#password").FillAsync("pepe123_P");
            await page.Locator("#login").ClickAsync();
            var locator = page.Locator(".welcome_menu").First;
            await Assertions.Expect(locator).ToHaveTextAsync(
                new Regex("Welcome to Adactin Group of Hotels"));
            await page.CloseAsync();
        }
    }
}