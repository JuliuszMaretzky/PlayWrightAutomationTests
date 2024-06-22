using Microsoft.Playwright;

namespace PlayWright_POM
{
    public class SearchPage
    {
        private readonly IPage _page;
        private readonly ILocator locationDd;
        private readonly ILocator hotelsDd;
        private readonly ILocator roomTypeDd;
        private readonly ILocator noOfRoomDd;
        private readonly ILocator checkinDateTxt;
        private readonly ILocator checkoutDateTxt;
        private readonly ILocator adultPerRoomDd;
        private readonly ILocator searchBtn;

        public SearchPage(IPage page)
        {
            _page = page;
            locationDd = _page.Locator("#location");
            searchBtn = _page.Locator("#Submit");
        }

        public async Task GoToAsync()
        {
            await _page.GotoAsync("https://adactinhotelapp.com/SearchHotel.php");
        }

        public async Task SearchHotel(string location)
        {
            await locationDd.TypeAsync(location);
            await searchBtn.ClickAsync();
        }
    }
}