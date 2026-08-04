using OpenQA.Selenium;

namespace RepairShopTracker.Tests.Pages
{
    public class EditOrderPage
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl = "http://localhost:5093/RepairOrders/Edit";

        private By ClientNameField => By.Id("clientName");
        private By ReportedIssueField => By.Id("reportedIssue");
        private By UpdateButton => By.Id("btnUpdate");
        private By ValidationErrors => By.CssSelector(".text-danger");

        public EditOrderPage(IWebDriver driver) => _driver = driver;

        public void NavigateToEdit(string id) => _driver.Navigate().GoToUrl($"{_baseUrl}/{id}");

        public void UpdateClientName(string newName)
        {
            var field = _driver.FindElement(ClientNameField);
            field.Clear();
            field.SendKeys(newName);
        }

        public void ClearClientName() => _driver.FindElement(ClientNameField).Clear();

        public void SetReportedIssue(string text)
        {
            var field = _driver.FindElement(ReportedIssueField);
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].value = arguments[1];", field, text);
        }

        public void Save()
        {
            _driver.FindElement(UpdateButton).Click();
            Thread.Sleep(1000);
        }

        public bool HasValidationErrors() => _driver.FindElements(ValidationErrors).Count > 0;
    }
}