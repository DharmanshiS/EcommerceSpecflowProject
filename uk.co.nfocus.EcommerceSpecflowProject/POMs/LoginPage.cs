﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class LoginPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public LoginPage(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _usernameField => _driver.FindElement(By.CssSelector("#username"));
        private IWebElement _passwordField => _driver.FindElement(By.CssSelector("#password"));
        private IWebElement _loginButton => _driver.FindElement(By.CssSelector("#customer_login > div.u-column1.col-1 > form > p:nth-child(3) > button"));
        private IWebElement _dismissButton => _driver.FindElement(By.LinkText("Dismiss"));
        

        //Service Methods
        public LoginPage SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            return this;
        }

        public LoginPage SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }

        public void SubmitForm()
        {
            _loginButton.Click();
        }

        //Helper methods
        public void ClickDismiss()
        {
            _dismissButton.Click(); //remove the warning at the bottom to avoid missing any elements on the screen
        }
        public void Login(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            SubmitForm();
        }
    }
}