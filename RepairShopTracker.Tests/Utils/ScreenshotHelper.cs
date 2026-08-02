using OpenQA.Selenium;

namespace RepairShopTracker.Tests.Utils
{
    public static class ScreenshotHelper
    {
        public static string TakeScreenshot(IWebDriver driver, string testName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Screenshots");
            Directory.CreateDirectory(folder);

            string fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string fullPath = Path.Combine(folder, fileName);

            screenshot.SaveAsFile(fullPath);
            return fullPath;
        }
    }
}