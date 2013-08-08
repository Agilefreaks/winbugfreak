namespace BugFreak.Silverlight.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using global::BugFreak.Components;

    [TestClass]
    public class BugFreakTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            GlobalConfig.Settings.Token = "token";
            GlobalConfig.Settings.ApiKey = "apiKey";
            GlobalConfig.Settings.ServiceEndPoint = "http://service.co";
        }

        [TestMethod]
        public void Hook_Always_AddsSilverlightErrorDataProvider()
        {
            BugFreak.Hook();

            Assert.IsInstanceOfType(GlobalConfig.ErrorDataProviders.First(), typeof(SilverlightErrorDataProvider));
        }
    }
}