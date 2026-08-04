using NUnit.Framework;
using OpenQA.Selenium;
using RepairShopTracker.Tests.Pages;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests.Tests
{
    [TestFixture]
    public class CreateOrderTests
    {
        private IWebDriver _driver = null!;
        private LoginPage _loginPage = null!;
        private CreateOrderPage _createOrderPage = null!;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateDriver();
            _loginPage = new LoginPage(_driver);
            _createOrderPage = new CreateOrderPage(_driver);

            _loginPage.Login("admin", "Admin123!");
        }

        [Test]
        public void CrearOrden_CaminoFeliz_DatosValidos_DeberiaGuardarse()
        {
            _createOrderPage.CreateOrder("Juan Perez", "Lavadora", "No enciende", 1500);

            Assert.That(_driver.Url, Does.Contain("/RepairOrders"),
                "Debería redirigir al listado tras crear la orden exitosamente");
            Assert.That(_driver.PageSource, Does.Contain("Juan Perez"),
                "El nuevo cliente debería aparecer en el listado");
        }

        [Test]
        public void CrearOrden_PruebaNegativa_CamposObligatoriosVacios_DeberiaMostrarError()
        {
            _createOrderPage.Navigate();
            _createOrderPage.Save();

            Assert.That(_createOrderPage.HasValidationErrors(), Is.True,
                "Debería mostrar errores de validación si los campos obligatorios están vacíos");
        }

        [Test]
        public void CrearOrden_PruebaLimites_FallaReportadaExcedeLimite_DeberiaMostrarError()
        {
            string textoLargo = new string('A', 600);

            _createOrderPage.CreateOrder("Cliente Prueba", "Radio", textoLargo, 100);

            Assert.That(_createOrderPage.HasValidationErrors(), Is.True,
                "Debería mostrar error de validación si la falla reportada excede el límite de 500 caracteres");
        }

        [TearDown]
        public void Teardown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            ScreenshotHelper.TakeScreenshot(_driver, testName);

            Thread.Sleep(10000);

            _driver.Quit();
            _driver.Dispose();
        }
    }
}