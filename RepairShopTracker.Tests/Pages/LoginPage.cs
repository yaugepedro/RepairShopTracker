using OpenQA.Selenium;

namespace RepairShopTracker.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "http://localhost:5093/Account/Login";

        private By UsernameField => By.Id("username");
        private By PasswordField => By.Id("password");
        private By LoginButton => By.Id("btnLogin");
        private By ErrorMessage => By.CssSelector(".text-danger");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            Thread.Sleep(Utils.TestConfig.StepDelayMs);
        }

        public void EnterUsername(string username)
        {
            _driver.FindElement(UsernameField).SendKeys(username);
            Thread.Sleep(Utils.TestConfig.StepDelayMs);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(PasswordField).SendKeys(password);
            Thread.Sleep(Utils.TestConfig.StepDelayMs);
        }

        public void ClickLogin()
        {
            _driver.FindElement(LoginButton).Click();
            Thread.Sleep(Utils.TestConfig.StepDelayMs);
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
            return _driver.FindElements(ErrorMessage).Count > 0;
        }
    }
}