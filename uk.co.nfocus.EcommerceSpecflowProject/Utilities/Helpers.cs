using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace uk.co.nfocus.EcommerceSpecflowProject.Utilities
{
    internal class Helpers
    {
        public static IWebElement Wait(IWebDriver driver, By locator, int timeToWait = 3)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(timeToWait));
            IWebElement element = wait.Until(drv => drv.FindElement(locator));
            return element;
        }

        public static string CaptureScreenshot(IWebDriver driver)
        {
            string Timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string FileName = $"pointOfFailure_{Timestamp}.png";
            string ProjectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string FilePath = Path.Combine(ProjectDirectory, FileName);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(FilePath);
            return FilePath;
        }


        //sending keys

    }
}
