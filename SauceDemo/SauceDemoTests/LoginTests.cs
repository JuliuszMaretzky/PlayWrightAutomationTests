using Microsoft.Playwright.NUnit;
using SauceDemo.SauceDemoTests;

namespace SauceDemo
{
    public class LoginTests : TestSetup
    {
        private const string password = "secret_sauce";

        [Test]
        [TestCase("standard_user")]
        [TestCase("locked_out_user")]
        [TestCase("problem_user")]
        [TestCase("performance_glitch_user")]
        [TestCase("error_user")]
        [TestCase("visual_user")]
        public async Task Check_If_All_Logins_On_Login_Page_Work_Properly(string login)
        {
            await LoginPage.GoToLoginPage();
            await LoginPage.TypeCredentials(login, password);
            await LoginPage.ClickLogin();
        }
    }
}