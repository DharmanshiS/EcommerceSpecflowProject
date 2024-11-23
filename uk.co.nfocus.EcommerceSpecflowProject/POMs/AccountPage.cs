using OpenQA.Selenium;
namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class AccountPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public AccountPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _accountTitle => _driver.FindElement(By.CssSelector("#post-7 > header > h1"));

        //Service Methods
        public string GetAccountTitle()
        {
            return _accountTitle.Text;
        }
    }
}
