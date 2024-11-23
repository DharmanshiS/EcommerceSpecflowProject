using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class OrdersPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public OrdersPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _firstOrderNumber => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));

        //Service Methods
        public string GetLatestOrderNumber()
        {
            return _firstOrderNumber.Text.Remove(0,1); //remove the '#' from the beginning so matching is easier
        }
    }
}
