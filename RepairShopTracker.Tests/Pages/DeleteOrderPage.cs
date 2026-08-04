using OpenQA.Selenium;

namespace RepairShopTracker.Tests.Pages
{
    public class DeleteOrderPage
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl = "http://localhost:5093/RepairOrders/Delete";

        private By ConfirmDeleteButton => By.Id("btnConfirmDelete");
        private By CancelLink => By.CssSelector("a.btn-secondary");

        public DeleteOrderPage(IWebDriver driver) => _driver = driver;

        public void NavigateToDelete(string id) => _driver.Navigate().GoToUrl($"{_baseUrl}/{id}");

        public void ConfirmDelete()
        {
            _driver.FindElement(ConfirmDeleteButton).Click();
            Thread.Sleep(1000);
        }

        public void Cancel() => _driver.FindElement(CancelLink).Click();
    }
}