using OpenQA.Selenium;
using uk.co.nfocus.EcommerceSpecflowProject.Utilities;

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

        private IWebElement _viewCartFromProduct() => Helpers.Wait(_driver, By.CssSelector($"a[title='View cart']"), 3);


        // Service Methods

        public void AddProductToCart(string product)
        {
            _addProductToCart(product).Click();
        }

        public void GoToCart()
        {
            _viewCartFromProduct().Click();
        }
    }
}
