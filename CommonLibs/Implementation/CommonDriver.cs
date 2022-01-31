using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace CommonLibs.Implementation
{
    public class CommonDriver
    {
        /** 
        ThreadLocal is used to make IWebdriver instance as threadsafe. 
        i.e. for parallel execution **/

        //Initialization of selenium webdriver(Make it thread safe)
        public ThreadLocal<IWebDriver> _driver { get; private set; }

        //Initialize the Thread safe value to Driver refference variable
        public IWebDriver Driver => _driver.Value;

        //Threshold value for the page to get loaded on a page
        private int pageLoadTimeout;

        //Wait time for the element to be detected. i.e. Implicit wait in selenium
        private int elementDetectionTimeout;

        //Getter and Setter for PageLoadTimeout
        public int PageLoadTimeout
        {
            private get { return pageLoadTimeout; }
            set { if (value >= 0) { pageLoadTimeout = value; } }
        }

        //Getter and Setter for ElementDetectionTimeout
        public int ElementDetectionTimeout
        {

            private get { return elementDetectionTimeout; }

            set
            {
                if (value > 0) { elementDetectionTimeout = value; }
            }
        }

        //Constructor of this class. Here we set the default values of timeouts and logic to invoke a browser instance
        public CommonDriver(string browserType)
        {
            browserType = browserType.Trim().ToLower();
             
            pageLoadTimeout = 60;
            elementDetectionTimeout = 10;

            if (browserType.Equals("chrome"))
            {
                _driver = new ThreadLocal<IWebDriver>(() => new ChromeDriver());

                /*ChromeOptions options = new ChromeOptions();
                _driver = new ThreadLocal<IWebDriver>(() => new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options));*/
            }
            else if (browserType.Equals("chrome_headless"))
            {
                //Disable GPU rendering, extensions, and disabling pop-up extensions in developer mode
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-gpu");
                options.AddArguments("--disable-extensions");
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-dev-shm-usage");
                options.AddArguments("--headless");
                options.AddArguments("--window-size=1366,768");

                _driver = new ThreadLocal<IWebDriver>(() => new ChromeDriver(options));

                /*// instead of this url you can put the url of your remote hub
                _driver = new ThreadLocal<IWebDriver>(() => new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options));*/
            }
            else if (browserType.Equals("edge"))
            {
                _driver = new ThreadLocal<IWebDriver>(() => new EdgeDriver());

                /*EdgeOptions options = new EdgeOptions();
                _driver = new ThreadLocal<IWebDriver>(() => new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options));*/
            }
            else if (browserType.Equals("firefox"))
            {
                _driver = new ThreadLocal<IWebDriver>(() => new FirefoxDriver());

                /*FirefoxOptions options = new FirefoxOptions();
                _driver = new ThreadLocal<IWebDriver>(() => new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options)); */
                
            }
            else
            {
                throw new Exception("Invalid browser type -" + browserType);
            }

            //To delete all cookies
            Driver.Manage().Cookies.DeleteAllCookies();

            //To maximize the size of the browser to fit window
            Driver.Manage().Window.Maximize();
        }

        //Method to navigate to a given entry point url of the application and set page load and implicit timeouts
        public void NavigateToFirstUrl(string url)
        {
            url = url.Trim();

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(elementDetectionTimeout);

            Driver.Url = url;
        }

        /**Some common driver methods are defined here**/

        //Method to refresh the page
        public void Refresh() => Driver.Navigate().Refresh();

        //Method to close all browsers
        public void CloseAllBrowsers() => Driver.Quit();

        //Method to close the current open window
        public void CloseBrowser() => Driver.Close();

        //Method to get current url of the window open
        public string GetCurrentUrl() => Driver.Url;

        //Method to get current page source of the window open
        public string GetPageSource() => Driver.PageSource;

        //Method to get current page title
        public string GetTitle() => Driver.Title;

        //Method to navigate back from the current page
        public void NavigateBackward() => Driver.Navigate().Back();

        //Method to navigate forward from the current page
        public void NavigateForward() => Driver.Navigate().Forward();
    }

}

