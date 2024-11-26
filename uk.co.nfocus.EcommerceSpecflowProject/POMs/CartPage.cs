using OpenQA.Selenium;
using System.Text.RegularExpressions;
using uk.co.nfocus.EcommerceSpecflowProject.Utilities;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class CartPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public CartPage(IWebDriver driver) 
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _couponBox => _driver.FindElement(By.CssSelector("#coupon_code"));
        private IWebElement _applyCouponButton => _driver.FindElement(By.CssSelector("button[value='Apply coupon']"));
        private IWebElement _couponSuccessMessage => Utilities.Helpers.Wait(_driver, By.CssSelector("#post-5 > div > div > div.woocommerce-notices-wrapper > div"), 5);
        private IWebElement _cartSubtotal => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span > bdi"));
        private IWebElement _cartTotalCouponDiscount => Utilities.Helpers.Wait(_driver, By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span"), 5);
        private IWebElement _cartTotalShipping => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi"));
        private IWebElement _cartTotalTotal => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span > bdi"));
        private IWebElement _checkoutButton => _driver.FindElement(By.CssSelector(".checkout-button.button.alt.wc-forward"));
        

        //Service Methods
        public void AddCoupon(string coupon)
        {
            Helpers.FillInputBox(_couponBox, coupon);
            _applyCouponButton.Click();
        }
        public string GetCouponSuccessMessage()
        {
            return _couponSuccessMessage.Text;

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
                    itemsPresent = false; // No more delete buttons found
                }
            } while (itemsPresent);
        }

        public decimal GetCartSubtotal()
        {
            return decimal.Parse(Regex.Replace(_cartSubtotal.Text, @"[^\d.]", ""));
        }
        public decimal GetCartTotalCouponDiscount()
        {
            return decimal.Parse(Regex.Replace(_cartTotalCouponDiscount.Text, @"[^\d.]", ""));
        }
        public decimal GetCartTotalShipping()
        {
            return decimal.Parse(Regex.Replace(_cartTotalShipping.Text, @"[^\d.]", ""));
        }
        public decimal GetCartTotal()
        {
            return decimal.Parse(Regex.Replace(_cartTotalTotal.Text, @"[^\d.]", ""));
        }

    }
}
