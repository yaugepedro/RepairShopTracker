using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly string _url =
            "http://localhost:5093/Account/Login";

        private By UsernameField => By.Id("username");
        private By PasswordField => By.Id("password");
        private By LoginButton => By.Id("btnLogin");
        private By ErrorMessage => By.CssSelector(".text-danger");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;

            _wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(10));

            _wait.PollingInterval =
                TimeSpan.FromMilliseconds(200);

            _wait.IgnoreExceptionTypes(
                typeof(StaleElementReferenceException),
                typeof(NoSuchElementException));
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(_url);

            _wait.Until(driver =>
            {
                var field = driver.FindElement(UsernameField);
                return field.Displayed && field.Enabled;
            });

            Pause();
        }

        public void EnterUsername(string username)
        {
            var field = _wait.Until(driver =>
                driver.FindElement(UsernameField));

            field.Clear();
            field.SendKeys(username);

            Pause();
        }

        public void EnterPassword(string password)
        {
            var field = _wait.Until(driver =>
                driver.FindElement(PasswordField));

            field.Clear();
            field.SendKeys(password);

            Pause();
        }

        public void ClickLogin()
        {
            var button = _wait.Until(driver =>
                driver.FindElement(LoginButton));

            button.Click();

            _wait.Until(driver =>
            {
                if (driver.Url.Contains(
                    "/RepairOrders",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                return HasVisibleError(driver);
            });

            Pause();
        }

        public void Login(string username, string password)
        {
            Navigate();
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public bool HasErrorMessage()
        {
            return HasVisibleError(_driver);
        }

        private bool HasVisibleError(IWebDriver driver)
        {
            try
            {
                return driver.FindElements(ErrorMessage)
                    .Any(element =>
                    {
                        try
                        {
                            return element.Displayed &&
                                   !string.IsNullOrWhiteSpace(
                                       element.Text);
                        }
                        catch (StaleElementReferenceException)
                        {
                            return false;
                        }
                    });
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        private static void Pause()
        {
            if (TestConfig.StepDelayMs > 0)
            {
                Thread.Sleep(TestConfig.StepDelayMs);
            }
        }
    }
}