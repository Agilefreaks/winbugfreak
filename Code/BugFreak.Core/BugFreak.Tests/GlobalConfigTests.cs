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
            
            AgileReporter.Init();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.Settings.AppName = null;
            GlobalConfig.Settings.Token = null;
        }

        [Test]
        public void ServiceProviderGet_Always_ReturnsInstance()
        {
            var result = GlobalConfig.ServiceProvider;

            Assert.IsNotNull(result);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIReportRequestBuilder_ReturnsInstanceOfReportRequestBuilder()
        {
            var result = GlobalConfig.ServiceProvider.GetService(typeof (IReportRequestBuilder));

            Assert.AreEqual(typeof(ReportRequestBuilder), result.GetType());
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportQueue_ReturnsInstance()
        {
            var result = GlobalConfig.ServiceProvider.GetService(typeof (IErrorReportQueue));

            Assert.IsNotNull(result);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportQueue_ReturnsSameInstance()
        {
            var instance1 = GlobalConfig.ServiceProvider.GetService(typeof (IErrorReportQueue));

            var instance2 = GlobalConfig.ServiceProvider.GetService(typeof (IErrorReportQueue));

            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void ServiceProvider_GetServiceOfTypeIErrorReportSerializer_ReturnsInstanceFromGlobalConfigSerializer()
        {
            var serializer = GlobalConfig.ErrorReportSerializer;

            var instance = GlobalConfig.ServiceProvider.GetService(typeof (IErrorReportSerializer));

            Assert.AreSame(serializer, instance);
        }
    }
}
