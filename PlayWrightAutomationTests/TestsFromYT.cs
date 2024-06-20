//https://www.youtube.com/watch?v=d31_UcGIac8&list=PLyR3u3h9srduMQ0G2SefuMz3Dkkkzs59k&index=2

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

        [Test]
        public async Task Trace_TC005()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1
            });
            var context = await browser.NewContextAsync();

            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true, 
                Sources = true
            });

            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "Pepeusz123");
            await page.FillAsync("#password", "pepe123_P");
            await page.ClickAsync("#login");

            await page.TypeAsync("#location", "Sydney");
            await page.ClickAsync("#Submit");
            await page.ClickAsync("#radiobutton_1");
            await page.ClickAsync("#continue");

            await context.Tracing.StopAsync(new()
            {
                Path = "trace/trace.zip"
            });
            await context.CloseAsync();
            await browser.CloseAsync();
            //you can view trace by powershell: cd [project path\bin\Debug\net6.0]
            //.playwright.ps1 show-trace "[project path\bin\Debug\net6.0\trace\trace.zip]"
            //or drag'n'drop trace.zip into https://trace.playwright.dev
        }

        [Test]
        public async Task SaveState_TC006()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1
            });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "Pepeusz123");
            await page.FillAsync("#password", "pepe123_P");
            await page.ClickAsync("#login");

            await context.StorageStateAsync(new()
            {
                Path = @"state\state_login.json"
            });

            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [Test]
        public async Task RetrieveState_TC006()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1
            });
            var context = await browser.NewContextAsync(new()
            {
                StorageStatePath = @"state\state_login.json"
            });

            var page = await context.NewPageAsync();

            Thread.Sleep(3000);

            await page.GotoAsync("https://adactinhotelapp.com/SearchHotel.php");
            await page.TypeAsync("#location", "Sydney");

            await page.GotoAsync("https://adactinhotelapp.com/SelectHotel.php");
            await page.ClickAsync("#continue");
        }

        [Test]
        public async Task Login_CodeGen_TC007()
        {
            //in powershell: .\playwright.ps1 codegen https://adactinhotelapp.com/
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.Locator("#username").FillAsync("Pepeusz123");
            await page.Locator("#password").FillAsync("pepe123_P");
            await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
            await page.Locator("#location").SelectOptionAsync(new[] { "Sydney" });
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
            await page.Locator("#radiobutton_2").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            await page.Locator("#first_name").ClickAsync();
            await page.Locator("#first_name").FillAsync("Pepeusz");
            await page.Locator("#first_name").PressAsync("Tab");
            await page.Locator("#last_name").FillAsync("Pepeuszowski");
            await page.Locator("#address").ClickAsync();
            await page.Locator("#address").FillAsync("Pepe Address 12/3");
            await page.Locator("#cc_num").ClickAsync();
            await page.Locator("#cc_num").FillAsync("7418529630789512");
            await page.Locator("#cc_type").SelectOptionAsync(new[] { "AMEX" });
            await page.Locator("#cc_exp_month").SelectOptionAsync(new[] { "2" });
            await page.Locator("#cc_exp_year").SelectOptionAsync(new[] { "2025" });
            await page.Locator("#cc_cvv").ClickAsync();
            await page.Locator("#cc_cvv").FillAsync("123");
            await page.GetByRole(AriaRole.Button, new() { Name = "Book Now" }).ClickAsync();
        }
    }
}