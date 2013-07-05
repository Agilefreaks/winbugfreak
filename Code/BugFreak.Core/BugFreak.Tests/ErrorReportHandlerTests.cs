using System;
using BugFreak;
using BugFreak.Components;
using Moq;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class ErrorReportHandlerTests
    {
        private Mock<IRemoteErrorReportStorage> _mockRemoteErrorReportStorage;
        private Mock<ILocalErrorReportStorage> _mockLocalErrorReportStorage;
        private Mock<IServiceProvider> _mockServiceProvider;
        private ErrorReportHandler _subject;

        [SetUp]
        public void SetUp()
        {
            _mockRemoteErrorReportStorage = new Mock<IRemoteErrorReportStorage>();
            _mockLocalErrorReportStorage = new Mock<ILocalErrorReportStorage>();
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockServiceProvider.Setup(m => m.GetService(typeof (IRemoteErrorReportStorage)))
                                .Returns(_mockRemoteErrorReportStorage.Object);
            _mockServiceProvider.Setup(m => m.GetService(typeof (ILocalErrorReportStorage)))
                                .Returns(_mockLocalErrorReportStorage.Object);

            GlobalConfig.ServiceProvider = _mockServiceProvider.Object;

            _subject = new ErrorReportHandler();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceProvider = null;
        }

        [Test]
        public void Ctor_Always_CallsServiceProviderGetServiceOfTypeIRemoteErrorReportStorage()
        {
            _mockServiceProvider.Verify(m => m.GetService(typeof(IRemoteErrorReportStorage)));
        }

        [Test]
        public void Ctor_Always_CallsServiceProviderGetServiceOfTypeILocalErrorReportStorage()
        {
            _mockServiceProvider.Verify(m => m.GetService(typeof(ILocalErrorReportStorage)));
        }

        [Test]
        public void Handl_Always_CallsRemoteStorage()
        {
            var errorReport = new ErrorReport();

            _subject.Handle(errorReport);

            _mockRemoteErrorReportStorage.Verify(m => m.TryStore(errorReport));
        }

        [Test]
        public void Handle_WhenRemoteStorageReturnsFalse_CallsLocalStorage()
        {
            var errorReport = new ErrorReport();
            
            _subject.Handle(errorReport);

            _mockLocalErrorReportStorage.Verify(m => m.TryStore(errorReport));
        }

        [Test]
        public void Handle_WhenRemoteStorageReturnsTrue_DoesNotCallLocalStorage()
        {
            var errorReport = new ErrorReport();
            _mockRemoteErrorReportStorage.Setup(m => m.TryStore(errorReport))
                                         .Returns(true);

            _subject.Handle(errorReport);

            _mockLocalErrorReportStorage.Verify(m => m.TryStore(It.IsAny<ErrorReport>()), Times.Never());
        }
    }
}
