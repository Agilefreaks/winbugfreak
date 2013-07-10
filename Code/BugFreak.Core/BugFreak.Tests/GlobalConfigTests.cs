using BugFreak;
using BugFreak.Components;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class GlobalConfigTests
    {
        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Settings.AppName = "app";
            GlobalConfig.Settings.Token = "v2.2";
            GlobalConfig.Settings.ApiKey = "apiKey";
            GlobalConfig.Settings.ServiceEndPoint = "http://myTest.com";
            
            AgileReporter.Init();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.Settings.AppName = null;
            GlobalConfig.Settings.Token = null;
            GlobalConfig.Settings.ApiKey = null;
            GlobalConfig.Settings.ServiceEndPoint = null;
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
            var result = GlobalConfig.ServiceLocator.GetService<IErrorReportQueue>();

            Assert.IsNotNull(result);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportQueue_ReturnsSameInstance()
        {
            var instance1 = GlobalConfig.ServiceLocator.GetService<IErrorReportQueue>();

            var instance2 = GlobalConfig.ServiceLocator.GetService<IErrorReportQueue>();

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
