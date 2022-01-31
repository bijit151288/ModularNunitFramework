using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPages.Pages
{
    public class DashboardPage : BasePage
    {
        //Webdriver instance
        private IWebDriver driver;

        /**Add all the webelements of this page**/
        private IWebElement Link_marketplace => driver.FindElement(By.XPath("//input[@id='MP_link']"));
        private IWebElement EmployeeDistributionSubunit_Panel => driver.FindElement(By.XPath("//legend[text()='Employee Distribution by Subunit']//ancestor::fieldset"));
        private IWebElement Legend_Panel => driver.FindElement(By.XPath("//legend[text()='Legend']//ancestor::fieldset"));
        private IWebElement PendingLeaveRequests_Panel => driver.FindElement(By.XPath("//legend[text()='Pending Leave Requests']//ancestor::fieldset"));
        private IWebElement UserNameLabel(string name) => driver.FindElement(By.XPath("//a[@id='welcome' and text()='" + name + "']"));

        //Constructor of the page class:
        public DashboardPage(IWebDriver driver)
        {
            //Initialize the webdriver instance
            this.driver = driver;
        }

        /**Page actions: features(behavior) of the page the form of methods**/
        public string VerifyDashboardPageTitle()
        {
            return driver.Title;
        }

        public bool VerifyCorrectUserNameByName(String name)
        {
            return commonElement.IsElementVisible(UserNameLabel(name));
        }

        public bool ValidateEmployeeDistributionSubunitPanel()
        {
            return commonElement.IsElementVisible(EmployeeDistributionSubunit_Panel);
        }

        public bool ValidateLegendPanel()
        {
            return commonElement.IsElementVisible(Legend_Panel);
        }

        public bool ValidatePendingLeaveRequestsPanel()
        {
            return commonElement.IsElementVisible(PendingLeaveRequests_Panel);
        }
    }
}
