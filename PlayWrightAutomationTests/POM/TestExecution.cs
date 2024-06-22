using Microsoft.Playwright.NUnit;

namespace PlayWright_POM
{
    [TestFixture]
    public class TestExecution : PageTest
    {
        [Test]
        public async Task Booking_TC001()
        {
            LoginPage loginPage = new LoginPage(Page);
            SearchPage searchPage = new SearchPage(Page);
            SelectPage selectPage = new SelectPage(Page);
            BookingPage bookingPage = new BookingPage(Page);

            await loginPage.Login("https://adactinhotelapp.com/index.php", "Pepeusz123", "pepe123_P");
            await searchPage.SearchHotel("Sydney");
            await selectPage.SelectHotel();
            await bookingPage.BookHotel("Pepeusz", "Pepeuszowski", "Pepe 12/3 Pepewo", "1234567891234560", "VISA", "1", "2137", "123");
        }
    }
}