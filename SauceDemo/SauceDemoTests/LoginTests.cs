using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using SauceDemo.SauceDemoTests;

namespace SauceDemo.SauceDemoTests
{
    public class LoginTests : TestSetup
    {
        private const string standardPassword = "secret_sauce";
        private const string standardUsername = "standard_user";

        [Test]
        [TestCase(standardUsername)]
        [TestCase("locked_out_user")]
        [TestCase("problem_user")]
        [TestCase("performance_glitch_user")]
        [TestCase("error_user")]
        [TestCase("visual_user")]
        public async Task Check_If_All_Usernames_On_Login_Page_Work_Properly(string login)
        {
            await LoginPage.GoToLoginPage();
            await LoginPage.FillCredentials(login, standardPassword);
            await LoginPage.ClickLogin();
            await InventoryPage.WaitForLoad();
        }

        [Test]
        [TestCase(standardUsername)]
        [TestCase(standardPassword)]
        public async Task Check_If_Login_Requires_Both_Username_And_Password(string filledCredential)
        {
            await LoginPage.GoToLoginPage();
            switch (filledCredential)
            {
                case standardUsername:
                    await LoginPage.FillUsername(standardUsername);
                    await LoginPage.AssertFilledPassword(string.Empty);
                    await LoginPage.ClickLogin("Password");
                    break;
                case standardPassword:
                    await LoginPage.FillPassword(standardPassword);
                    await LoginPage.AssertFilledUsername(string.Empty);
                    await LoginPage.ClickLogin("Username");
                    break;
            }
        }
    }
}