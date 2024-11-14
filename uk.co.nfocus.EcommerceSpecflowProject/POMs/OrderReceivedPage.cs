﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceSpecflowProject.POMs
{
    internal class OrderReceivedPage
    {
        private IWebDriver _driver; //Field to hold webdriver for page interactions


        //Constructor
        public OrderReceivedPage(IWebDriver driver) //Get the driver from the calling test
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _orderNumber => Utilities.Helpers.Wait(_driver, By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"), 7);


        //Service methods
        public string GetOrderNumber()
        {
            return _orderNumber.Text;
        }
    }
}