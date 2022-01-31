using CommonLibs.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IO;

namespace ApplicationTests.Tests
{
    [SetUpFixture]
    public class ExtentReportSetup
    {
        public static ExtentReport extentReport;
        private string reportFilename;
        private string workingDirectory;
        private IConfigurationRoot _configuration;
        private string TestDataFilename;

        //Will run only once when the class is loaded
        [OneTimeSetUp]
        public void PreSetup()
        {
            //Create the current project directory
            workingDirectory = Directory.GetCurrentDirectory();

            //Set the filename of the testdata excel sheet
            TestDataFilename = $"{workingDirectory}/ApplicationTests/TestData/TestDataSheet.xls";

            //Fetch testdata from the file and the sheet as provided
            ExcelDriver.PopulateInCollection(TestDataFilename, "TestData");

            ///Passing a variable "environmentName" as a Parameter to the NUnit Console Runner 
            ///To set the environment name. Valid Values are: Dev, QA, Staging and Prod(case-insensitive)
            var environmentName = TestContext.Parameters.Get("EnvironmentName", "QA");
            environmentName = environmentName.Trim().ToLower();

            //Initilaize _configuration reference variable and set the appSetting.json path
            var builder = new ConfigurationBuilder().AddJsonFile($"{workingDirectory}/ApplicationTests/appSettings_{environmentName}.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

            //Initilaize reportFilename variable and set the index.html path
            reportFilename = $"{workingDirectory}/Reports/index.html";

            //Provide report name here
            string ReportName = "Application_Name Test Report";

            //Initialize the extentReport and provide the file name(reportFilename)
            extentReport = new ExtentReport(reportFilename, ReportName);

            //Get current OS/Version and Hostname
            OperatingSystem os = Environment.OSVersion;

            string OS_Version = os.VersionString.ToString();
            string OS_Platform = os.Platform.ToString();
            string Hostname = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

            //Call the AddSysInfo to provide OS info and hostname
            extentReport.AddSysInfo("Operating System", OS_Version);
            extentReport.AddSysInfo("Platform", OS_Platform);
            extentReport.AddSysInfo("Hostname", Hostname);

            //Call the AddSysInfo to provide browser info
            extentReport.AddSysInfo("Browser", _configuration["browserType"].ToUpper());
        }

        [OneTimeTearDown]
        public void PostTearDown()
        {
            //Reset the excel data collection to reload the collection
            ExcelDriver.ResetDataCollection();

            //Flush all utilities of extent report to generate the report
            extentReport.FlushExtentReports();
        }

    }
}
