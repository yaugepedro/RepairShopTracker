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
            // Descomenta la siguiente línea si quieres correr sin ventana visible:
            // options.AddArgument("--headless");

            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return driver;
        }
    }
}