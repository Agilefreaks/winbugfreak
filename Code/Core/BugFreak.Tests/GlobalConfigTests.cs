using BugFreak.Components;
using NUnit.Framework;

namespace BugFreak.Tests
{
    [TestFixture]
    public class GlobalConfigTests
    {
        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Token = "v2.2";
            GlobalConfig.ApiKey = "apiKey";
            GlobalConfig.ServiceEndPoint = "http://myTest.com";

            ReportingService.Init();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.Token = null;
            GlobalConfig.ApiKey = null;
            GlobalConfig.ServiceEndPoint = null;
        }

        [Test]
        public void ErrorDataProviders_IsNotNull()
        {
            Assert.IsNotNull(GlobalConfig.ErrorDataProviders);
        }

        [Test]
        public void ServiceProviderGet_Always_ReturnsInstance()
        {
            var result = GlobalConfig.ServiceLocator;

            Assert.IsNotNull(result);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIReportRequestBuilder_ReturnsInstanceOfReportRequestBuilder()
        {
            var result = GlobalConfig.ServiceLocator.GetService<IReportRequestBuilder>();

            Assert.AreEqual(typeof(ReportRequestBuilder), result.GetType());
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportQueue_ReturnsInstance()
        {
            var result = GlobalConfig.ServiceLocator.GetService<IErrorQueue>();

            Assert.IsNotNull(result);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportQueue_ReturnsSameInstance()
        {
            var instance1 = GlobalConfig.ServiceLocator.GetService<IErrorQueue>();

            var instance2 = GlobalConfig.ServiceLocator.GetService<IErrorQueue>();

            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportSerializer_ReturnsInstanceFromGlobalConfigSerializer()
        {
            var serializer = GlobalConfig.ErrorReportSerializer;

            var instance = GlobalConfig.ServiceLocator.GetService<IErrorReportSerializer>();

            Assert.AreSame(serializer, instance);
        }
    }
}
