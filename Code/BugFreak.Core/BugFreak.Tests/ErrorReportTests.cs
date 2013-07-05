using System;
using System.Linq;
using BugFreak;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class ErrorReportTests
    {
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
            exception.Data.Add("myKey", "myValue");

            var result = ErrorReport.FromException(exception);

            Assert.AreEqual("myValue", result.AdditionalData.First(kvp => kvp.Key == "myKey").Value);
        }
    }
}
