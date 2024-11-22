using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class NavigationBar
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions

        //Constructor
        public NavigationBar(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _shopNavigation => _driver.FindElement(By.CssSelector("#menu-item-43 > a"));
        private IWebElement _logoutNavigation => _driver.FindElement(By.CssSelector("#post-7 > div > div > nav > ul > li.woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--customer-logout > a"));
        private IWebElement _ordersNavigation => _driver.FindElement(By.CssSelector("#post-7 > div > div > nav > ul > li.woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--orders > a"));
        private IWebElement _myAccountNavigation => _driver.FindElement(By.CssSelector("#menu-item-46 > a"));
        private IWebElement _cartSymbol => _driver.FindElement(By.CssSelector("#site-header-cart > li:nth-child(1) > a"));
        private IWebElement _cartMessage => _driver.FindElement(By.CssSelector("#site-header-cart > li:nth-child(2) > div > div > p"));



        //Service Methods
        public void NavigateToShop()
        {
            _shopNavigation.Click();
        }

        public void NavigateToOrders()
        {
            _ordersNavigation.Click();
        }
        public void NavigateToMyAccount()
        {
            _myAccountNavigation.Click();
        }
        public void Logout()
        {
            _logoutNavigation.Click();
        }
        public void HoverOverCartSymbol()
        {
            Actions action = new Actions(_driver); // Perform the hover action
            action.MoveToElement(_cartSymbol).Perform();
        }
        public void ViewCartFromSymbol()
        {
            _cartSymbol.Click();
        }
        public string GetCartEmptyMessage()
        {
            HoverOverCartSymbol();
            return _cartMessage.Text;
        }
    }
}
