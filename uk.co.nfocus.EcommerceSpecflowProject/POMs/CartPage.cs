using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class CartPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public CartPage(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _couponBox => _driver.FindElement(By.CssSelector("#coupon_code"));
        private IWebElement _applyCouponButton => _driver.FindElement(By.CssSelector("button[value='Apply coupon']"));
        
        //for checking coupon success - assertion
        public string CouponSuccessMessageAlreadyThere
        {
            get => Utilities.Helpers.Wait(_driver, By.CssSelector("#post-5 > div > div > div.woocommerce-notices-wrapper > ul > li"), 5).Text;

        }
        public string CouponSuccessMessage
        {
            get => Utilities.Helpers.Wait(_driver, By.CssSelector("#post-5 > div > div > div.woocommerce-notices-wrapper > div"), 5).Text;

        }

        //all Cart Total fields for assertions
        public decimal CartTotalSubtotal
        {
            get => decimal.Parse(Regex.Replace(_driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi")).Text, @"[^\d.]", ""));
        }
        public decimal CartTotalCouponDiscount
        {
            get => decimal.Parse(Regex.Replace(Utilities.Helpers.Wait(_driver, By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span"), 5).Text, @"[^\d.]", ""));
        }
        public decimal CartTotalShipping
        {
            get => decimal.Parse(Regex.Replace(_driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi")).Text, @"[^\d.]", ""));
        }
        public decimal CartTotalTotal
        {
            get => decimal.Parse(Regex.Replace(_driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span > bdi")).Text, @"[^\d.]", ""));
        }
        private IWebElement _checkoutButton => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > div > a"));
        

        //Service Methods
        public void AddCoupon(string coupon)
        {
            _couponBox.Clear();
            _couponBox.SendKeys(coupon);
            _applyCouponButton.Click();
        }

        public void Checkout()
        {
            _checkoutButton.Click();
        }

        public void DeleteAllItems()
        {     
            bool itemsPresent;
            do
            {
                try
                {
                    var deleteButton = _driver.FindElement(By.LinkText("×")); // Find the first delete button and click it
                    deleteButton.Click();
                    Thread.Sleep(1000); // Wait for the item to be removed
                    itemsPresent = _driver.FindElements(By.LinkText("×")).Count > 0; // Check if there are more delete buttons
                }
                catch (NoSuchElementException)
                {
                    // No more delete buttons found
                    itemsPresent = false;
                }
            } while (itemsPresent);
        }

    }
}
