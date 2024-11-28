using OpenQA.Selenium;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class OrderReceivedPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public OrderReceivedPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _orderNumber => Utilities.Helpers.Wait(_driver, By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"), 7);


        //Service methods
        public string GetOrderNumber()
        {
            return _orderNumber.Text;
        }
    }
}
