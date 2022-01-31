using OpenQA.Selenium;
using System;
using System.IO;

namespace CommonLibs.Utils
{
    public class ScreenshotUtils
    {
        //Create instance (camera) of an interface called ITakesScreenshot
        ITakesScreenshot camera;

        public ScreenshotUtils(IWebDriver driver)
        {
            //Typecast camera to driver instance
            camera = (ITakesScreenshot)driver;
        }
        public void CaptureAndSaveScreenshot(string Filename)
        {
            //Trim the filename and save it in same name (underscore is used for that purpose to save it in same name)
            _ = Filename.Trim();

            //If the file already exist then throw exception.
            if (File.Exists(Filename))
            {
                throw new Exception("Filename already exist.." + Filename);
            }

            //Create instance of class Screenshot. This will get the screenshot
            Screenshot screenshot = camera.GetScreenshot();

            //Save the screenshot with the file name given
            screenshot.SaveAsFile(Filename);

        }
    }
}
