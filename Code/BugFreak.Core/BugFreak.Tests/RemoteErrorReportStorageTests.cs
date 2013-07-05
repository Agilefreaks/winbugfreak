using System;
using System.Net;
using BugFreak;
using BugFreak.Components;
using Moq;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class RemoteErrorReportStorageTests
    {
        private RemoteErrorReportStorage _subject;
        private Mock<IServiceProvider> _mockServiceProvider;
        private Mock<IReportRequestBuilder> _mockReportRequestBuilder;

        [SetUp]
        public void SetUp()
        {
            _mockReportRequestBuilder = new Mock<IReportRequestBuilder>();
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockServiceProvider.Setup(m => m.GetService(typeof (IReportRequestBuilder)))
                                .Returns(_mockReportRequestBuilder.Object);
            GlobalConfig.ServiceProvider = _mockServiceProvider.Object;

            _subject = new RemoteErrorReportStorage();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceProvider = null;
        }

        [Test]
        public void Ctor_Always_CallsServiceProviderGetInstanceOfTypeIReportRequestBuilder()
        {
            _mockServiceProvider.Verify(m => m.GetService(typeof(IReportRequestBuilder)));
        }

        [Test]
        public void TryStore_Always_CallsReportRequestBuilderBuildWithReport()
        {
            var errorReport = new ErrorReport();

            _subject.TryStore(errorReport);

            _mockReportRequestBuilder.Verify(m => m.Build(errorReport));
        }

        [Test]
        public void TryStore_WhenRequestBuilderRaisesException_ReturnsFalse()
        {
            var errorReport = new ErrorReport();
            _mockReportRequestBuilder.Setup(m => m.Build(errorReport))
                                     .Throws<Exception>();

            var result = _subject.TryStore(errorReport);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryStore_WhenGetResponseRaisesException_ReturnsFalse()
        {
            var errorReport = new ErrorReport();
            var mockRequest = new Mock<WebRequest>();
            mockRequest.Setup(m => m.GetResponse())
                       .Throws<Exception>();
            _mockReportRequestBuilder.Setup(m => m.Build(errorReport))
                                     .Returns(mockRequest.Object);

            var result = _subject.TryStore(errorReport);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryStore_WhenNoException_ReturnsTrue()
        {
            var errorReport = new ErrorReport();
            var mockRequest = new Mock<WebRequest>();
            
            _mockReportRequestBuilder.Setup(m => m.Build(errorReport))
                                     .Returns(mockRequest.Object);

            var result = _subject.TryStore(errorReport);

            Assert.IsTrue(result);
        }
    }
}
