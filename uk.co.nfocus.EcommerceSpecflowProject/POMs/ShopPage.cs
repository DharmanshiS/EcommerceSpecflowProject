using OpenQA.Selenium;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class ShopPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public ShopPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _addProductToCart(string product) => _driver.FindElement(By.CssSelector($"a[aria-label='Add “{product}” to your cart']"));


        // Service Methods
        
        public void AddProductToCart(string product)
        {
            _addProductToCart(product).Click();
        }
    }
}
