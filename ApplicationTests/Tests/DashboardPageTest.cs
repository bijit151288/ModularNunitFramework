using ApplicationPages.Pages;
using AventStack.ExtentReports;
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
    public class DashboardPageTest : BaseTests
    {    
        //Dashboard page object reference
        DashboardPage dashboardPage;

        [SetUp]
        public void DashboardPageSetUp()
        {
            //Initialize DashboardPage object reference by performing login operation
            dashboardPage = loginPage.DoLogin(ExcelDriver.ReadData(1, "Username"), ExcelDriver.ReadData(1, "Password"));
        }

        [Test]
        public void VerifyUserNameTest()
        {
            ExtentReportSetup.extentReport.CreateATestCase("Verify Username Test");
            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Validating user name displayed correctly in dashboard page");
            ExtentReportSetup.extentReport.AssignTestCategory("Dashboard Page Tests");

            bool flag = dashboardPage.VerifyCorrectUserNameByName(ExcelDriver.ReadData(1, "PanelUsername"));
            Assert.True(flag, "Username displayed in dashboard page is not correct");
        }

        [Test]
        public void VerifyEmployeeDistributionSubunitPanelTest()
        {
            ExtentReportSetup.extentReport.CreateATestCase("Verify Employee Distribution Subunit Panel Test");
            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Validating Employee Distribution Subunit Panel presence");
            ExtentReportSetup.extentReport.AssignTestCategory("Dashboard Page Tests");

            bool flag = dashboardPage.ValidateEmployeeDistributionSubunitPanel();
            Assert.True(flag);
        }

        [Test]
        public void VerifyLegendPanelTest()
        {
            ExtentReportSetup.extentReport.CreateATestCase("Verify Legend Panel Test");
            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Validating Legend Panel presence");
            ExtentReportSetup.extentReport.AssignTestCategory("Dashboard Page Tests");

            bool flag = dashboardPage.ValidateLegendPanel();
            Assert.True(flag);
        }

        [Test]
        public void VerifyPendingLeaveRequestsPanelTest()
        {
            ExtentReportSetup.extentReport.CreateATestCase("Verify Pending Leave Requests Panel Test");
            ExtentReportSetup.extentReport.AddTestLog(Status.Info, "Validating Pending Leave Request Panel presence");
            ExtentReportSetup.extentReport.AssignTestCategory("Dashboard Page Tests");

            bool flag = dashboardPage.ValidatePendingLeaveRequestsPanel();
            Assert.True(flag);
        }
    }
}
