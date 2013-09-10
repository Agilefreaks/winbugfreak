using System.Linq;
using BugFreak.Components;
using NUnit.Framework;

namespace BugFreak.WinRT.Tests
{
    [TestFixture]
    public class GlobalConfigTests
    {
        [SetUp]
        public void SetUp()
        {
            GlobalConfig.ApiKey = "apiKey";
            GlobalConfig.Token = "token";
        }

        [Test]
        public void GetServicesIErrorReportStorage_Always_ReturnsRemoteErrorReportStorage()
        {
            ReportingService.Init();
            
            var storage = GlobalConfig.ServiceLocator.GetServices<IErrorReportStorage>();

            Assert.IsTrue(storage.Any(s => s is RemoteErrorReportStorage));
        }
    }
}
