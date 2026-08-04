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
        }

        public void FillForm(string clientName, string applianceType, string reportedIssue, decimal? cost = null)
        {
            if (!string.IsNullOrEmpty(clientName))
                _driver.FindElement(ClientNameField).SendKeys(clientName);

            if (!string.IsNullOrEmpty(applianceType))
                _driver.FindElement(ApplianceTypeField).SendKeys(applianceType);

            if (!string.IsNullOrEmpty(reportedIssue))
            {
                // Usamos JavaScript para asignar el valor directamente y
                // saltarnos el atributo maxlength="500" que el navegador
                // agrega automáticamente por el [StringLength(500)] del modelo.
                // Así podemos probar que la validación del SERVIDOR
                // realmente rechaza texto que excede el límite.
                var field = _driver.FindElement(ReportedIssueField);
                var js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].value = arguments[1];", field, reportedIssue);
            }

            if (cost.HasValue)
                _driver.FindElement(CostField).SendKeys(cost.Value.ToString());
        }

        public void Save()
        {
            _driver.FindElement(SaveButton).Click();
            Thread.Sleep(1000);
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