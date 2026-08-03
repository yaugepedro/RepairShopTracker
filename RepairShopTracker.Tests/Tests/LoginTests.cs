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
        private LoginPage _loginPage = null!;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateDriver();
            _loginPage = new LoginPage(_driver);
        }

        [Test]
        public void Login_CaminoFeliz_CredencialesValidas_DeberiaIngresar()
        {
            _loginPage.Login("admin", "Admin123!");

            Assert.That(_driver.Url, Does.Contain("/RepairOrders"),
                "El usuario debería ser redirigido a la lista de órdenes tras un login exitoso");
        }

        [Test]
        public void Login_PruebaNegativa_ContrasenaIncorrecta_DeberiaMostrarError()
        {
            _loginPage.Login("admin", "ClaveIncorrecta123");

            Assert.That(_loginPage.HasErrorMessage(), Is.True,
                "Debería mostrarse un mensaje de error con credenciales inválidas");
            Assert.That(_driver.Url, Does.Contain("/Account/Login"),
                "El usuario debería permanecer en la página de login");
        }

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

            Thread.Sleep(10000);

            _driver.Quit();
            _driver.Dispose();
        }
    }
}