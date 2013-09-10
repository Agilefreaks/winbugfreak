namespace BugFreak.Tests
{
    using System;
    using System.Collections.Generic;
    using BugFreak.Components;
    using BugFreak.Framework;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ErrorReportHandlerTests
    {
        private Mock<IErrorReportStorage> _mockRemoteErrorReportStorage;
        private Mock<IErrorReportStorage> _mockLocalErrorReportStorage;
        private Mock<IServiceLocator> _mockServiceProvider;
        private ErrorQueue _queue;
        private ErrorHandler _subject;

        [SetUp]
        public void SetUp()
        {
            _mockRemoteErrorReportStorage = new Mock<IErrorReportStorage>();
            _mockLocalErrorReportStorage = new Mock<IErrorReportStorage>();
            _mockServiceProvider = new Mock<IServiceLocator>();
            _queue = new ErrorQueue();
            _mockServiceProvider.Setup(m => m.GetServices<IErrorReportStorage>())
                                .Returns(new List<IErrorReportStorage> { _mockRemoteErrorReportStorage.Object, _mockLocalErrorReportStorage.Object });
            _mockServiceProvider.Setup(m => m.GetService<IErrorQueue>())
                                .Returns(_queue);

            GlobalConfig.ServiceLocator = _mockServiceProvider.Object;

            _subject = new ErrorHandler();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceLocator = null;
        }

        [Test]
        public void Handl_Always_CallsRemoteStorage()
        {
            var exception = new Exception();

            _subject.Handle(exception, null);

            _mockRemoteErrorReportStorage.Verify(m => m.SaveAsync(It.IsAny<ErrorReport>()));
        }

        [Test]
        public void Handle_WhenRemoteStorageReturnsFalse_CallsLocalStorage()
        {
            var exception = new Exception();
            _mockRemoteErrorReportStorage.Setup(m => m.SaveAsync(It.IsAny<ErrorReport>()))
                .Raises(m => m.SaveCompleted += null, new ErrorReportSaveCompletedEventArgs { Success = false });

            _subject.Handle(exception, null);

            _mockLocalErrorReportStorage.Verify(m => m.SaveAsync(It.IsAny<ErrorReport>()));
        }

        [Test]
        public void Handle_WhenRemoteStorageReturnsTrue_DoesNotCallLocalStorage()
        {
            var exception = new Exception();
            _mockRemoteErrorReportStorage.Setup(m => m.SaveAsync(It.IsAny<ErrorReport>()))
                .Raises(m => m.SaveCompleted += null, new ErrorReportSaveCompletedEventArgs { Success = true });

            _subject.Handle(exception, null);

            _mockLocalErrorReportStorage.Verify(m => m.SaveAsync(It.IsAny<ErrorReport>()), Times.Never());
        }
    }
}
