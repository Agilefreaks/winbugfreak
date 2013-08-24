using System;
using System.Linq;
using NUnit.Framework;

namespace BugFreak.Tests
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using BugFreak.Components;

    [TestFixture]
    public class ErrorReportTests
    {
        private class MockErrorDataProvider : IErrorDataProvider
        {
            public List<KeyValuePair<string, string>> GetData()
            {
                return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("test", "test")};
            }
        }

        [Test]
        public void FromException_WhenExceptionIsNull_ReturnsNull()
        {
            var result = ErrorReport.FromException(null);

            Assert.IsNull(result);
        }

        [Test]
        public void FromException_Always_SetsMessage()
        {
            var exception = new Exception("message");

            var result = ErrorReport.FromException(exception);

            Assert.AreEqual("message", result.Message);
        }

        [Test]
        public void FromException_Always_AddsAdditionalData()
        {
            var exception = new Exception();
            GlobalConfig.ErrorDataProviders.Add(new MockErrorDataProvider());

            var result = ErrorReport.FromException(exception);

            Assert.AreEqual("test", result.AdditionalData.First(kvp => kvp.Key == "test").Value);
        }
    }
}
