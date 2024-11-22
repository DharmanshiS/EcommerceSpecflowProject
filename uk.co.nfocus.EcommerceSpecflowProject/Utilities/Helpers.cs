using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace uk.co.nfocus.EcommerceSpecflowProject.Utilities
{
    internal class Helpers
    {
        public static IWebElement Wait(IWebDriver driver, By locator, int timeToWait = 3)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(timeToWait));
            IWebElement element = wait.Until(drv => drv.FindElement(locator));
            return element;
        }

        //screenshots
        //sending keys

    }
}
