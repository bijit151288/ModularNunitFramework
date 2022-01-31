using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CommonLibs.Utils
{
    public class WaitUtils
    {
        public static void WaitTillElementVisible(IWebDriver driver, By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

            wait.Until(c => c.FindElement(by));

        }

        public static void WaitTillAlertIsPresent(IWebDriver driver, int timeoutInSeconds)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.IgnoreExceptionTypes(typeof(NoAlertPresentException));

            wait.Until(c => c.SwitchTo().Alert());
        }
    }
}
