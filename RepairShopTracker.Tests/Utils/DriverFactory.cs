using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RepairShopTracker.Tests.Utils
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();

            
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-first-run");
            options.AddArgument("--no-default-browser-check");
            options.AddArgument("--disable-extensions");

            IWebDriver driver = new ChromeDriver(options);

            driver.Manage().Timeouts().ImplicitWait =
                TimeSpan.Zero;

            driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(30);

            return driver;
        }
    }
}