using OpenQA.Selenium;

namespace RepairShopTracker.Tests.Pages
{
    public class RepairOrdersIndexPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "http://localhost:5093/RepairOrders";

        private By TableRows => By.CssSelector("#ordersTable tbody tr");

        public RepairOrdersIndexPage(IWebDriver driver) => _driver = driver;

        public void Navigate() => _driver.Navigate().GoToUrl(_url);

        public string? GetRowIdByClientName(string clientName)
        {
            var row = _driver.FindElements(TableRows)
                .FirstOrDefault(r => r.Text.Contains(clientName));

            return row?.GetAttribute("id")?.Replace("row-", "");
        }

        public bool ContainsClient(string clientName) => _driver.PageSource.Contains(clientName);
    }
}