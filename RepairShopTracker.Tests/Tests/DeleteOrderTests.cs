using NUnit.Framework;
using OpenQA.Selenium;
using RepairShopTracker.Tests.Pages;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests.Tests
{
    [TestFixture]
    public class DeleteOrderTests
    {
        private IWebDriver _driver = null!;
        private LoginPage _loginPage = null!;
        private CreateOrderPage _createOrderPage = null!;
        private RepairOrdersIndexPage _indexPage = null!;
        private DeleteOrderPage _deleteOrderPage = null!;
        private string _clientName = null!;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateDriver();
            _loginPage = new LoginPage(_driver);
            _createOrderPage = new CreateOrderPage(_driver);
            _indexPage = new RepairOrdersIndexPage(_driver);
            _deleteOrderPage = new DeleteOrderPage(_driver);

            _clientName = $"ClienteEliminar_{Guid.NewGuid():N}".Substring(0, 22);

            _loginPage.Login("admin", "Admin123!");
            _createOrderPage.CreateOrder(_clientName, "Radio", "No prende", 200);
        }

        [Test]
        public void EliminarOrden_CaminoFeliz_OrdenExistente_DeberiaEliminarse()
        {
            _indexPage.Navigate();
            var id = _indexPage.GetRowIdByClientName(_clientName);
            _deleteOrderPage.NavigateToDelete(id!);
            _deleteOrderPage.ConfirmDelete();

            Assert.That(_driver.Url, Does.Contain("/RepairOrders"));
            Assert.That(_driver.PageSource, Does.Not.Contain(_clientName));
        }

        [Test]
        public void EliminarOrden_PruebaNegativa_Cancelar_DeberiaConservarOrden()
        {
            _indexPage.Navigate();
            var id = _indexPage.GetRowIdByClientName(_clientName);
            _deleteOrderPage.NavigateToDelete(id!);
            _deleteOrderPage.Cancel();

            _indexPage.Navigate();
            Assert.That(_driver.PageSource, Does.Contain(_clientName),
                "La orden no debería eliminarse si se cancela la acción");
        }

        [Test]
        public void EliminarOrden_PruebaLimites_IdInexistente_DeberiaMostrarError()
        {
            _deleteOrderPage.NavigateToDelete("999999");

            Assert.That(_driver.PageSource, Does.Not.Contain(_clientName));
        }

        [TearDown]
        public void Teardown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            ScreenshotHelper.TakeScreenshot(_driver, testName);

            Thread.Sleep(TestConfig.EndOfTestDelayMs);

            _driver.Quit();
            _driver.Dispose();
        }
    }
}