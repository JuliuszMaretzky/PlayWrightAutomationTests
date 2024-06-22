using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoPages
{
    public class LoginPage
    {
        private const string pageAddress = "https://www.saucedemo.com/";
        private readonly IPage _page;
        private readonly ILocator usernameTextBox;
        private readonly ILocator passwordTextBox;
        private readonly ILocator loginButton;
        private readonly ILocator loginPageHeader;

        public LoginPage(IPage page)
        {
            _page = page;
            usernameTextBox = _page.Locator("//input[@data-test='username']");
            passwordTextBox = _page.Locator("//input[@data-test='password']");
            loginButton = _page.Locator("//input[@data-test='login-button']");
            loginPageHeader = _page.Locator("//*[@class='login_logo']");
        }

        public async Task GoToLoginPage()
        {
            await _page.GotoAsync(pageAddress);
            await Assertions.Expect(loginPageHeader).ToHaveTextAsync("Swag Labs");
            await Assertions.Expect(usernameTextBox).ToBeVisibleAsync();
            await Assertions.Expect(passwordTextBox).ToBeVisibleAsync();
            await Assertions.Expect(loginButton).ToBeVisibleAsync();
        }

        public async Task TypeCredentials(string username, string password)
        {
            await usernameTextBox.FillAsync(username);
            await passwordTextBox.FillAsync(password);
        }

        public async Task ClickLogin()
        {
            await loginButton.ClickAsync();
        }
    }
}
