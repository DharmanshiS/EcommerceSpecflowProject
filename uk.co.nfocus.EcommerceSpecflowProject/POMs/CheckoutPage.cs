using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceSpecflowProject.Utilities;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class CheckoutPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public CheckoutPage(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        public string AccountTitle
        {
            get => _driver.FindElement(By.CssSelector("#post-7 > header > h1")).Text;
        }
        private IWebElement _firstNameField => _driver.FindElement(By.CssSelector("#billing_first_name"));
        private IWebElement _lastNameField => _driver.FindElement(By.CssSelector("#billing_last_name"));
        private IWebElement _streetAddressField => _driver.FindElement(By.CssSelector("#billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.CssSelector("#billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.CssSelector("#billing_postcode"));
        private IWebElement _phoneField => _driver.FindElement(By.CssSelector("#billing_phone"));
        private IWebElement _checkPaymentsField => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        private IWebElement _placeOrderButton => Utilities.Helpers.Wait(_driver, By.CssSelector("#place_order"), 15);

        //Service methods
        public void FillBillingDetails(BillingDetails details)
        {
            _firstNameField.Clear();
            _firstNameField.SendKeys(details.FirstName);

            _lastNameField.Clear();
            _lastNameField.SendKeys(details.LastName);

            _streetAddressField.Clear();
            _streetAddressField.SendKeys(details.Street);

            _cityField.Clear();
            _cityField.SendKeys(details.City);

            _postcodeField.Clear();
            _postcodeField.SendKeys(details.Postcode);

            _phoneField.Clear();
            _phoneField.SendKeys(details.Phone);

            //_checkPaymentsField.Click();

        }

        public void PlaceOrder()
        {

            /*
             * This gives a StaleElementException - Needed to use JavaScript to click on the button.
             * Seems like the Place order button is using ajax to render the screen. Even though the button can be seen,
             * it is not clickable. 
            */

            //_driver.FindElement(By.LinkText("Place order")).Click();  
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", _driver.FindElement(By.CssSelector("#place_order")));
        }
    }
}
