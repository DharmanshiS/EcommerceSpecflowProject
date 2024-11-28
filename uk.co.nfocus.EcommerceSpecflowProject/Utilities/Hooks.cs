using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using uk.co.nfocus.EcommerceSpecflowProject.POMs;
using uk.co.nfocus.EcommerceSpecflowProject.Utilities;
using Allure.Net.Commons;
using System.IO;

namespace uk.co.nfocus.EcommerceProject.Utilities
{
    [Binding]
    public class Hooks
    {
        
        private IWebDriver _driver; //field to share driver between class methods
        private readonly WebDriverWrapper _webDriverWrapper;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(WebDriverWrapper wrapper, ScenarioContext scenarioContext) //Constructor will be run by Specflow when it instantiates this class to use the [Before] step. When it does that it makes a ScenarioContext object and it is shared between the classes
        {
            _webDriverWrapper = wrapper; //the wrapper will be instanticated and passed in by Specflow
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
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                default:
                    _driver = new ChromeDriver();
                    break;
            }
            
            _webDriverWrapper.Driver = _driver; //Typesafe storage of WebDriver
            
            _driver.Manage().Window.Maximize();
            _driver.Url = TestContext.Parameters["baseURL"];
        }

        [After]
        public void Teardown()
        {
            if (_scenarioContext.TestError != null)
            {
                string[] Screenshot = Helpers.CaptureScreenshot(_driver);
                Console.WriteLine(Screenshot[0]);
                Console.WriteLine(Screenshot[1]);
                AllureApi.AddAttachment(Screenshot[0], "image/png", Screenshot[1]);
                
            }

            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.NavigateToMyAccount();
            NavigationBarPOM.Logout();
            Console.WriteLine("Successfully logged out.");

            _driver.Quit();
        }
    }
}






    
