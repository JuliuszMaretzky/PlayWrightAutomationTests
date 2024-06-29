using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.SauceDemoPages
{
    public class BasePage
    {
        protected IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
        }
    }
}
