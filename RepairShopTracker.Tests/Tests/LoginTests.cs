using NUnit.Framework;
using OpenQA.Selenium;
using RepairShopTracker.Tests.Pages;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests.Tests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver _driver = null!;
        private LoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateDriver();
            _loginPage = new LoginPage(_driver);
        }

        // Historia de Usuario 1: Login
        // Caso 1: Camino feliz - login con credenciales válidas
        [Test]
        public void Login_CaminoFeliz_CredencialesValidas_DeberiaIngresar()
        {
            _loginPage.Login("admin", "Admin123!");

            Assert.That(_driver.Url, Does.Contain("/RepairOrders"),
                "El usuario debería ser redirigido a la lista de órdenes tras un login exitoso");
        }

        // Caso 2: Prueba negativa - login con contraseña incorrecta
        [Test]
        public void Login_PruebaNegativa_ContrasenaIncorrecta_DeberiaMostrarError()
        {
            _loginPage.Login("admin", "ClaveIncorrecta123");

            Assert.That(_loginPage.HasErrorMessage(), Is.True,
                "Debería mostrarse un mensaje de error con credenciales inválidas");
            Assert.That(_driver.Url, Does.Contain("/Account/Login"),
                "El usuario debería permanecer en la página de login");
        }

        // Caso 3: Prueba de límites - campos vacíos
        [Test]
        public void Login_PruebaLimites_CamposVacios_DeberiaMostrarValidacion()
        {
            _loginPage.Login("", "");

            Assert.That(_driver.Url, Does.Contain("/Account/Login"),
                "El usuario no debería poder ingresar con campos vacíos");
        }

        [TearDown]
        public void Teardown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            ScreenshotHelper.TakeScreenshot(_driver, testName);

            _driver.Quit();
        }
    }
}