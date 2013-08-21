namespace BugFreak.WPF.Tests
{
    using System;
    using System.Linq;

    using NUnit.Framework;
    using global::BugFreak.Components;

    [TestFixture]
    public class BugFreakTests
    {
        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Token = "token";
            GlobalConfig.ApiKey = "apiKey";
            GlobalConfig.ServiceEndPoint = "http://service.co";
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.Token = null;
            GlobalConfig.ApiKey = null;
            GlobalConfig.ServiceEndPoint = null;
        }

        [Test]
        public void Hook_Always_AddsSilverlightErrorDataProvider()
        {
            try
            {
                BugFreak.Hook("apiKey", "token", null);
            }
            catch (NullReferenceException)
            {
            }

            Assert.IsInstanceOf<WpfErrorDataProvider>(GlobalConfig.ErrorDataProviders.First());
        }
    }
}