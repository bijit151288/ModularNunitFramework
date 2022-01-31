using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

namespace CommonLibs.Utils
{
    public class ExtentReport
    {
        ExtentHtmlReporter HtmlReporter;
        ExtentReports extentReports;
        ExtentTest extentTest;

        //Create the report
        public ExtentReport(string htmlReportFilename, string reportName)
        {
            //Initialize
            HtmlReporter = new ExtentHtmlReporter(htmlReportFilename);
            extentReports = new ExtentReports();     

            //Attach Html Report with extentReports
            extentReports.AttachReporter(HtmlReporter);

            //Provide the page title of the report here
            HtmlReporter.Config.DocumentTitle = "Tests Report";

            //Provide report name here
            HtmlReporter.Config.ReportName = reportName;
        }

        //This method will create a testcase name when called in
        public void CreateATestCase(string testcasename)
        {
            extentTest = extentReports.CreateTest(testcasename);
        }

        //This method will add a log at the log level provided
        public void AddTestLog(Status status, string comments)
        {
            extentTest.Log(status, comments);
        }

        //This method will attach the screenshot in the report
        public void AddScreenshotInReport(string filename)
        {
            extentTest.AddScreenCaptureFromPath(filename);
        }

        //Add system info to the dashboard of the report
        public void AddSysInfo(string name, string value)
        {
            extentReports.AddSystemInfo(name, value);
        }

        //Assign Category to the report
        public void AssignTestCategory(string categoryName)
        {
            extentTest.AssignCategory(categoryName);
        }

        //This is used to flush all extent report utilities at the end
        public void FlushExtentReports()
        {
            extentReports.Flush();
        }
    }
}
