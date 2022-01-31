using AventStack.ExtentReports;
using CommonLibs.Implementation;
using CommonLibs.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests.Tests
{
    //[Parallelizable(ParallelScope.Fixtures)]
    public class LoginPageTest : BaseTests
    {
        [Test]
        public void LoginPageTitleTest()
        {
            //Set Extent report test method name by using "CreateATestCase"
            ExtentReportSetup.extentReport.CreateATestCase("Login Page Title Test");
            //Add logs or test steps by using "AddTestLog"
            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Verify page title of the login page");
            //Assign Category to the test
            ExtentReportSetup.extentReport.AssignTestCategory("Login Page Tests");

            //Get the current page title
            string actualTitle = loginPage.GetLoginPageTitle();

            string expectedTitle = "OrangeHRMs";

            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Verify page title of the login page. Expected title: " + expectedTitle + " Actual title: " + actualTitle);
            Assert.AreEqual(expectedTitle, actualTitle, "The page title of the login page is not correct.");
        }

        [Test]
        public void VerifyLoginTest()
        {
            //Set Extent report test method name by using "CreateATestCase"
            ExtentReportSetup.extentReport.CreateATestCase("Verify Login Test");
            //Add logs or test steps by using "AddTestLog"
            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Perform login operation");
            //Assign Category to the test
            ExtentReportSetup.extentReport.AssignTestCategory("Login Page Tests");

            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Enter Username");
            loginPage.EnterUserName(ExcelDriver.ReadData(1, "Username"));

            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Enter Password");
            loginPage.EnterPassword(ExcelDriver.ReadData(1, "Password"));

            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Click on login button");
            loginPage.ClickOnLogin();

            string expectedTitle = "OrangeHRM";
            string actualTitle = cmnDriver.GetTitle();

            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Verify page title of the dashboard page. Expected title: " + expectedTitle + " Actual title: " + actualTitle);
            Assert.AreEqual(expectedTitle, actualTitle, "The page title of the dashboard page is not correct.");
        }
    }
}
