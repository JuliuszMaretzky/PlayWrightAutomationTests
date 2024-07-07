using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public virtual async Task WaitForLoad() { }

        public ILocator GetElementWithIndex(string itemXPath, int index)
        {
            return _page.Locator($"({itemXPath})[{index}]");
        }
    }
}
