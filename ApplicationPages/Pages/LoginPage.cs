using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPages.Pages
{
    public class LoginPage : BasePage
    {
        //Webdriver instance
        private IWebDriver driver;

        /**Add all the webelements of this page**/
        private IWebElement UserName => driver.FindElement(By.XPath("//input[@id='txtUsername']"));
        private IWebElement Password => driver.FindElement(By.XPath("//input[@id='txtPassword']"));
        private IWebElement SignInButton => driver.FindElement(By.XPath("//input[@id='btnLogin']"));
        private IWebElement OrangeHRMLogo => driver.FindElement(By.XPath("//img[contains(@src,'logo.png')]"));


        //Constructor of the page class:
        public LoginPage(IWebDriver driver)
        {
            //Initialize the webdriver instance
            this.driver = driver;
        }

        /**Page actions: features(behavior) of the page the form of methods**/
        public String GetLoginPageTitle()
        {
            return driver.Title;
        }

        public bool OrangeHRMLogoImageDisplayed()
        {
            return commonElement.IsElementVisible(OrangeHRMLogo);
        }

        public void EnterUserName(String username)
        {
            commonElement.SetText(UserName, username);
        }

        public void EnterPassword(String pwd)
        {
            commonElement.SetText(Password, pwd);
        }

        public void ClickOnLogin()
        {
            commonElement.ClickElement(SignInButton);
        }

        public DashboardPage DoLogin(String username, String pwd)
        {
            commonElement.SetText(UserName, username);
            commonElement.SetText(Password, pwd);
            commonElement.ClickElement(SignInButton);

            //This login method should return Dashboard Page class object.
            return new DashboardPage(driver);
        }

    }
}
