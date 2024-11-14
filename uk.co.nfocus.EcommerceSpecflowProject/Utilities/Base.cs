using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceSpecflowProject.POMs;




namespace uk.co.nfocus.EcommerceProject.Utilities
{
    [Binding]
    public class Base
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver; //field to share driver between class methods
        //private readonly WDWrapper _wdWrapper;

        public Base(ScenarioContext scenarioContext) //Constructor will be run by Specflow when it instantiates this class to use the [Before] step. When it does that it makes a ScenarioContext object and it is shared between the classes
        {
            //_wdWrapper = wdWrapper; //WDWrapper will be instanticated and passed in by Specflow
            _scenarioContext = scenarioContext;
        }

        [Before]
        public void SetUp()
        {
            string browser = TestContext.Parameters["browser"] ?? "chrome";

            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    break;

                default:
                    Assert.Fail("No browser set in runsettings");
                    break;
            }

            //_wdWrapper.Driver = _driver; //Typesafe storage of WebDriver
            _scenarioContext["webdriver"] = _driver;
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";


        }

        [After]
        public void Teardown()
        {
            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.NavigateToMyAccount();
            NavigationBarPOM.Logout();

            Thread.Sleep(5000); //Just there so we can see the browser before it quits
            _driver.Quit();

        }
    }
}






    
