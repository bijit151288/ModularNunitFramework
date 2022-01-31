using ApplicationPages.Pages;
using AventStack.ExtentReports;
using CommonLibs.Implementation;
using CommonLibs.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;

namespace ApplicationTests.Tests
{
    public class BaseTests
    {
        /**Create all instances required**/
        public CommonDriver cmnDriver;
        public LoginPage loginPage;
        public string workingDirectory;

        //This provides us utilities to read from external file like json
        //private vars will be started with underscore
        public IConfigurationRoot _configuration;

        public ScreenshotUtils screenshotUtils;

        private IWebDriver driver;

        //Will execute before every testcase
        [SetUp]
        public void SetUp()
        {
            //Create the current project directory
            workingDirectory = Directory.GetCurrentDirectory();

            //Passing a variable(environmentName) as a Parameter to the NUnit Console Runner
            ///Set the key "EnvironmentName". Valid Values are: Dev, QA, Staging and Prod
            var environmentName = TestContext.Parameters.Get("EnvironmentName", "QA");
            environmentName = environmentName.Trim().ToLower();

            //Initilaize _configuration reference variable and set the appSetting.json path
            var builder = new ConfigurationBuilder().AddJsonFile($"{workingDirectory}/ApplicationTests/appSettings_{environmentName}.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

            //_configuration = new ConfigurationBuilder().AddJsonFile(workingDirectory + "/ApplicationTests/appSettings.json").Build();

            //Get the browser type value from the appSettings.json file config.
            string browserType = _configuration["browserType"];

            //Initialize browser type
            cmnDriver = new CommonDriver(browserType);

            //Navigate to the base/entry point url. Get value from the appSettings.json file config.
            string baseUrl = _configuration["baseUrl"];

            cmnDriver.NavigateToFirstUrl(baseUrl);

            //Initialize the driver
            driver = cmnDriver.Driver;

            /*initialize LoginPage object reference. 
            Using loginPage object reference we can call all login methods written inside the LoginPage() class.*/
            loginPage = new LoginPage(driver);

            //Initialize the screenshotUtils
            screenshotUtils = new ScreenshotUtils(driver);
        }

        //Will execute after every testcase
        [TearDown]
        public void TearDown()
        {
            try
            {
                /*Incase of Nunit the status of test is stored in class called "TestContext". 
                "TestStatus" enum class indicates result of running a test.
                Get the fail status from here and take a screenshot to attach in the report*/

                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    ExtentReportSetup.extentReport.AddTestLog(Status.Fail, "Test case failed, please check logs or screenshots for failure reason");

                    string failMsg = TestContext.CurrentContext.Result.Message;
                    ExtentReportSetup.extentReport.AddTestLog(Status.Error, "The error captured is: "+failMsg);

                    string testcaseExecutionStartTime = DateUtils.GetCurrentDateAndTime();

                    //Concept of string interpolation i.e. whenever we want to pass any variable. ex: $""
                    string screenshotFile = $"{workingDirectory}/Screenshots/test-{testcaseExecutionStartTime}.jpeg";

                    screenshotUtils.CaptureAndSaveScreenshot(screenshotFile);

                    //Add screenshot file to the extent report
                    ExtentReportSetup.extentReport.AddScreenshotInReport(screenshotFile);
                }
                else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                {
                    ExtentReportSetup.extentReport.AddTestLog(Status.Pass, "Test Passed");
                }
                else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
                {
                    ExtentReportSetup.extentReport.AddTestLog(Status.Skip, "Test Skipped");
                }              
            }
            catch (Exception ex)
            {
                ExtentReportSetup.extentReport.AddTestLog(Status.Error, ex.StackTrace);
            }
            finally
            {
                //Close all browser windows open
                cmnDriver.CloseAllBrowsers();

            }
        }
    }
}
