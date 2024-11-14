using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class AccountPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public AccountPage(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        public string AccountTitle
        {
            get => _driver.FindElement(By.CssSelector("#post-7 > header > h1")).Text;
        }
    }
}
