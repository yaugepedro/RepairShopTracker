using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RepairShopTracker.Tests.Pages;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests.Tests
{
    [TestFixture]
    public class ReadOrdersTests
    {
        private IWebDriver _driver = null!;
        private LoginPage _loginPage = null!;
        private CreateOrderPage _createOrderPage = null!;
        private RepairOrdersIndexPage _indexPage = null!;
        private string _clientName = null!;

        [SetUp]
        public void Setup()
        {
            ExtentReportManager.CreateTest(TestContext.CurrentContext.Test.Name);

            _driver = DriverFactory.CreateDriver();
            _loginPage = new LoginPage(_driver);
            _createOrderPage = new CreateOrderPage(_driver);
            _indexPage = new RepairOrdersIndexPage(_driver);

            _clientName = $"ClienteListar_{Guid.NewGuid():N}".Substring(0, 20);

            _loginPage.Login("admin", "Admin123!");
            _createOrderPage.CreateOrder(_clientName, "TV", "No enciende", 300);
        }

        [Test]
        public void ListarOrdenes_CaminoFeliz_OrdenCreada_DeberiaAparecerEnListado()
        {
            _indexPage.Navigate();

            Assert.That(_indexPage.ContainsClient(_clientName), Is.True,
                "La orden recién creada debería aparecer en el listado");
        }

        [Test]
        public void ListarOrdenes_PruebaNegativa_ClienteInexistente_NoDeberiaAparecer()
        {
            _indexPage.Navigate();

            Assert.That(_indexPage.ContainsClient("Cliente_Que_No_Existe_XYZ999"), Is.False,
                "Un cliente que no fue creado no debería aparecer en el listado");
        }

        [Test]
        public void ListarOrdenes_PruebaLimites_NombreClienteConLongitudMaxima_DeberiaMostrarseCompleto()
        {
            string nombreLimite = new string('C', 100);
            _createOrderPage.CreateOrder(nombreLimite, "Radio", "No prende", 150);

            _indexPage.Navigate();

            Assert.That(_indexPage.ContainsClient(nombreLimite), Is.True,
                "El nombre con la longitud máxima permitida debería guardarse y mostrarse completo");
        }

        [TearDown]
        public void Teardown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            string screenshotPath = ScreenshotHelper.TakeScreenshot(_driver, testName);

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Passed)
            {
                ExtentReportManager.CurrentTest?.Pass("Prueba completada exitosamente.");
            }
            else if (status == TestStatus.Failed)
            {
                string message = TestContext.CurrentContext.Result.Message ?? "La prueba falló.";
                ExtentReportManager.CurrentTest?.Fail(message);
            }
            else
            {
                ExtentReportManager.CurrentTest?.Skip("Prueba omitida.");
            }

            ExtentReportManager.CurrentTest?.AddScreenCaptureFromPath(screenshotPath);

            Thread.Sleep(TestConfig.EndOfTestDelayMs);

            _driver.Quit();
            _driver.Dispose();
        }
    }
}