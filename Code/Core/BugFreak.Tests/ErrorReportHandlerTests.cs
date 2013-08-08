using System.Collections.Generic;
using System.Linq;
using Bugfreak;
using Bugfreak.Components;
using Bugfreak.Framework;
using Moq;
using NUnit.Framework;

namespace BugFreak.Tests
{
    [TestFixture]
    public class ErrorReportHandlerTests
    {
        private Mock<IErrorReportStorage> _mockRemoteErrorReportStorage;
        private Mock<IErrorReportStorage> _mockLocalErrorReportStorage;
        private Mock<IServiceLocator> _mockServiceProvider;
        private ErrorReportHandler _subject;

        [SetUp]
        public void SetUp()
        {
            _mockRemoteErrorReportStorage = new Mock<IErrorReportStorage>();
            _mockLocalErrorReportStorage = new Mock<IErrorReportStorage>();
            _mockServiceProvider = new Mock<IServiceLocator>();
            _mockServiceProvider.Setup(m => m.GetServices<IErrorReportStorage>())
                                .Returns(new List<IErrorReportStorage> { _mockRemoteErrorReportStorage.Object, _mockLocalErrorReportStorage.Object });

            GlobalConfig.ServiceLocator = _mockServiceProvider.Object;

            _subject = new ErrorReportHandler();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceLocator = null;
        }

        [Test]
        public void Ctor_Always_CallsServiceProviderGetServiceOfTypeIRemoteErrorReportStorage()
        {
            _mockServiceProvider.Verify(m => m.GetServices<IErrorReportStorage>());
        }

        [Test]
        public void Handl_Always_CallsRemoteStorage()
        {
            var errorReport = new ErrorReport();

            new SequentialResult(_subject.Handle(errorReport)).Execute(new ExecutionContext());

            _mockRemoteErrorReportStorage.Verify(m => m.SaveAsync(errorReport));
        }

        [Test]
        public void Handle_WhenRemoteStorageReturnsFalse_CallsLocalStorage()
        {
            var errorReport = new ErrorReport();
            _mockRemoteErrorReportStorage.Setup(m => m.SaveAsync(errorReport))
                .Raises(m => m.SaveCompleted += null, new ErrorReportSaveCompletedEventArgs { Success = false });

            new SequentialResult(_subject.Handle(errorReport)).Execute(new ExecutionContext());

            _mockLocalErrorReportStorage.Verify(m => m.SaveAsync(errorReport));
        }

        [Test]
        public void Handle_WhenRemoteStorageReturnsTrue_DoesNotCallLocalStorage()
        {
            var errorReport = new ErrorReport();
            _mockRemoteErrorReportStorage.Setup(m => m.SaveAsync(errorReport))
                .Raises(m => m.SaveCompleted += null, new ErrorReportSaveCompletedEventArgs { Success = true });

            _subject.Handle(errorReport).ToList();

            _mockLocalErrorReportStorage.Verify(m => m.SaveAsync(It.IsAny<ErrorReport>()), Times.Never());
        }
    }
}
