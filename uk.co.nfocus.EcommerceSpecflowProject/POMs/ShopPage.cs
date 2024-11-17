using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class ShopPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public ShopPage(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _firstAddToCart => _driver.FindElement(By.CssSelector("li:first-of-type > a.button"));
        private IWebElement _firstViewCart => Utilities.Helpers.Wait(_driver, By.CssSelector("#main > ul > li.product.type-product.post-27.status-publish.first.instock.product_cat-accessories.has-post-thumbnail.sale.shipping-taxable.purchasable.product-type-simple > a.added_to_cart.wc-forward"), 5);


        // Locators: Find the element you need to hover over
        private IWebElement _cartSymbol => _driver.FindElement(By.CssSelector("#site-header-cart > li:nth-child(1) > a"));

        public string CartMessage
        {
            get => _driver.FindElement(By.CssSelector("#site-header-cart > li:nth-child(2) > div > div > p")).Text;
        }

        // Service Methods
        public void AddToCartFromSymbol()
        {
            _cartSymbol.Click();
        }
        public void AddToCart()
        {
            _firstAddToCart.Click();
        }
        public void ViewCart()
        {
            _firstViewCart.Click();
        }
        public void HoverOverCartSymbol()
        {
            Actions action = new Actions(_driver); // Perform the hover action
            action.MoveToElement(_cartSymbol).Perform();
        }

        

    }
}
