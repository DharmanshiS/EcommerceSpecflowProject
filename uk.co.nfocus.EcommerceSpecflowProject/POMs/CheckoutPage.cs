using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using uk.co.nfocus.EcommerceSpecflowProject.Utilities;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class CheckoutPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public CheckoutPage(IWebDriver driver) 
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _firstNameField => _driver.FindElement(By.CssSelector("#billing_first_name"));
        private IWebElement _lastNameField => _driver.FindElement(By.CssSelector("#billing_last_name"));
        private IWebElement _streetAddressField => _driver.FindElement(By.CssSelector("#billing_address_1"));
        private IWebElement _cityField => _driver.FindElement(By.CssSelector("#billing_city"));
        private IWebElement _postcodeField => _driver.FindElement(By.CssSelector("#billing_postcode"));
        private IWebElement _phoneField => _driver.FindElement(By.CssSelector("#billing_phone"));

        //Service methods
        public void FillBillingDetails(BillingDetails details)
        {
            Helpers.FillInputBox(_firstNameField, details.FirstName);
            Helpers.FillInputBox(_lastNameField, details.LastName);
            Helpers.FillInputBox(_streetAddressField, details.Street);
            Helpers.FillInputBox(_cityField, details.City);
            Helpers.FillInputBox(_postcodeField, details.Postcode);
            Helpers.FillInputBox(_phoneField, details.Phone);
        }

        public void PlaceOrder()
        {
            /*
             * This gives a StaleElementException - Needed to use JavaScript to click on the button.
             * Seems like the Place order button is using ajax to render the screen. 
             * Even though the button can be seen, it is not clickable. 
            */          
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", _driver.FindElement(By.CssSelector("#place_order")));
        }
    }
}
