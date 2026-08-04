using OpenQA.Selenium;

namespace RepairShopTracker.Tests.Pages
{
    public class CreateOrderPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "http://localhost:5093/RepairOrders/Create";

        private By ClientNameField => By.Id("clientName");
        private By ApplianceTypeField => By.Id("applianceType");
        private By ReportedIssueField => By.Id("reportedIssue");
        private By StatusField => By.Id("status");
        private By CostField => By.Id("cost");
        private By SaveButton => By.Id("btnSave");
        private By ValidationErrors => By.CssSelector(".text-danger");

        public CreateOrderPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            Thread.Sleep(Utils.TestConfig.StepDelayMs);
        }

        public void FillForm(string clientName, string applianceType, string reportedIssue, decimal? cost = null)
        {
            if (!string.IsNullOrEmpty(clientName))
            {
                _driver.FindElement(ClientNameField).SendKeys(clientName);
                Thread.Sleep(Utils.TestConfig.StepDelayMs);
            }

            if (!string.IsNullOrEmpty(applianceType))
            {
                _driver.FindElement(ApplianceTypeField).SendKeys(applianceType);
                Thread.Sleep(Utils.TestConfig.StepDelayMs);
            }

            if (!string.IsNullOrEmpty(reportedIssue))
            {
                var field = _driver.FindElement(ReportedIssueField);
                var js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].value = arguments[1];", field, reportedIssue);
                Thread.Sleep(Utils.TestConfig.StepDelayMs);
            }

            if (cost.HasValue)
            {
                _driver.FindElement(CostField).SendKeys(cost.Value.ToString());
                Thread.Sleep(Utils.TestConfig.StepDelayMs);
            }
        }

        public void Save()
        {
            _driver.FindElement(SaveButton).Click();
            Thread.Sleep(Math.Max(1000, Utils.TestConfig.StepDelayMs));
        }

        public void CreateOrder(string clientName, string applianceType, string reportedIssue, decimal? cost = null)
        {
            Navigate();
            FillForm(clientName, applianceType, reportedIssue, cost);
            Save();
        }

        public bool HasValidationErrors()
        {
            return _driver.FindElements(ValidationErrors).Count > 0;
        }
    }
}