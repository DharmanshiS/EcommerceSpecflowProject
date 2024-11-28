using OpenQA.Selenium;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class LoginPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions

        //Constructor
        public LoginPage(IWebDriver driver) 
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _usernameField => _driver.FindElement(By.CssSelector("#username"));
        private IWebElement _passwordField => _driver.FindElement(By.CssSelector("#password"));
        private IWebElement _loginButton => _driver.FindElement(By.CssSelector("button[value='Log in']"));
        private IWebElement _dismissButton => _driver.FindElement(By.LinkText("Dismiss"));
        

        //Service Methods
        public void SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
        }

        public void SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
        }

        public void SubmitForm()
        {
            _loginButton.Click();
        }

        public void Login(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            SubmitForm();
        }

        public void ClickDismiss()
        {
            _dismissButton.Click(); //remove the warning at the bottom to avoid missing any elements on the screen
        }
    }
}
