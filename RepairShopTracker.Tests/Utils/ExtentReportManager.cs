using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace RepairShopTracker.Tests.Utils
{
    public static class ExtentReportManager
    {
        private static ExtentReports? _extent;
        private static readonly object _lock = new();

        public static ExtentReports Instance
        {
            get
            {
                if (_extent == null)
                {
                    lock (_lock)
                    {
                        if (_extent == null)
                        {
                            string reportsFolder = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Reports");
                            Directory.CreateDirectory(reportsFolder);

                            string reportPath = Path.Combine(
                                reportsFolder, $"ReporteEjecucion_{DateTime.Now:yyyyMMdd_HHmmss}.html");

                            var htmlReporter = new ExtentSparkReporter(reportPath);
                            htmlReporter.Config.DocumentTitle = "Reporte de Pruebas - RepairShopTracker";
                            htmlReporter.Config.ReportName = "Pruebas Automatizadas con Selenium";

                            _extent = new ExtentReports();
                            _extent.AttachReporter(htmlReporter);
                        }
                    }
                }
                return _extent;
            }
        }

        [ThreadStatic]
        private static ExtentTest? _currentTest;

        public static ExtentTest CreateTest(string testName, string? description = null)
        {
            _currentTest = Instance.CreateTest(testName, description);
            return _currentTest;
        }

        public static ExtentTest? CurrentTest => _currentTest;

        public static void Flush()
        {
            Instance.Flush();
        }
    }
}