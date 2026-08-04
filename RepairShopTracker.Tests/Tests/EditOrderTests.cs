using NUnit.Framework;
using OpenQA.Selenium;
using RepairShopTracker.Tests.Pages;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests.Tests
{
    [TestFixture]
    public class EditOrderTests
    {
        private IWebDriver _driver = null!;
        private LoginPage _loginPage = null!;
        private CreateOrderPage _createOrderPage = null!;
        private RepairOrdersIndexPage _indexPage = null!;
        private EditOrderPage _editOrderPage = null!;
        private string _clientName = null!;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateDriver();
            _loginPage = new LoginPage(_driver);
            _createOrderPage = new CreateOrderPage(_driver);
            _indexPage = new RepairOrdersIndexPage(_driver);
            _editOrderPage = new EditOrderPage(_driver);

            // Nombre único por test para evitar colisiones entre corridas
            _clientName = $"ClienteEditar_{Guid.NewGuid():N}".Substring(0, 20);

            _loginPage.Login("admin", "Admin123!");
            _createOrderPage.CreateOrder(_clientName, "Nevera", "No enfria", 800);
        }

        [Test]
        public void EditarOrden_CaminoFeliz_DatosValidos_DeberiaActualizarse()
        {
            _indexPage.Navigate();
            var id = _indexPage.GetRowIdByClientName(_clientName);
            _editOrderPage.NavigateToEdit(id!);

            string nuevoNombre = _clientName + "_Editado";
            _editOrderPage.UpdateClientName(nuevoNombre);
            _editOrderPage.Save();

            Assert.That(_driver.Url, Does.Contain("/RepairOrders"));
            Assert.That(_driver.PageSource, Does.Contain(nuevoNombre));
        }

        [Test]
        public void EditarOrden_PruebaNegativa_ClienteVacio_DeberiaMostrarError()
        {
            _indexPage.Navigate();
            var id = _indexPage.GetRowIdByClientName(_clientName);
            _editOrderPage.NavigateToEdit(id!);

            _editOrderPage.ClearClientName();
            _editOrderPage.Save();

            Assert.That(_editOrderPage.HasValidationErrors(), Is.True,
                "Debería mostrar error si el nombre del cliente queda vacío");
        }

        [Test]
        public void EditarOrden_PruebaLimites_FallaReportadaExcedeLimite_DeberiaMostrarError()
        {
            _indexPage.Navigate();
            var id = _indexPage.GetRowIdByClientName(_clientName);
            _editOrderPage.NavigateToEdit(id!);

            _editOrderPage.SetReportedIssue(new string('B', 600));
            _editOrderPage.Save();

            Assert.That(_editOrderPage.HasValidationErrors(), Is.True,
                "Debería mostrar error si la falla reportada excede 500 caracteres");
        }

        [TearDown]
        public void Teardown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            ScreenshotHelper.TakeScreenshot(_driver, testName);
            _driver.Quit();
            _driver.Dispose();
        }
    }
}