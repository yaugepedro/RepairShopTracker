using NUnit.Framework;
using RepairShopTracker.Tests.Utils;

namespace RepairShopTracker.Tests
{
    [SetUpFixture]
    public class GlobalTestSetup
    {
        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            ExtentReportManager.Flush();
        }
    }
}