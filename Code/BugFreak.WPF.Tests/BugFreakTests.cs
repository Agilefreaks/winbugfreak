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
            GlobalConfig.Settings.Token = "token";
            GlobalConfig.Settings.ApiKey = "apiKey";
            GlobalConfig.Settings.ServiceEndPoint = "http://service.co";
        }

        [Test]
        public void Hook_Always_AddsSilverlightErrorDataProvider()
        {
            try
            {
                BugFreak.Hook();
            }
            catch (NullReferenceException)
            {
            }

            Assert.IsInstanceOf<WpfErrorDataProvider>(GlobalConfig.ErrorDataProviders.First());
        }
    }
}