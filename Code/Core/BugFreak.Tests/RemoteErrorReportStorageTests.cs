using System.Linq;
using System.Net;
using BugFreak;
using BugFreak.Components;
using BugFreak.Framework;
using Moq;
using NUnit.Framework;

namespace BugFreak.Tests
{
    [TestFixture]
    public class RemoteErrorReportStorageTests
    {
        private RemoteErrorReportStorage _subject;
        private Mock<IServiceLocator> _mockServiceProvider;
        private Mock<IReportRequestBuilder> _mockReportRequestBuilder;

        [SetUp]
        public void SetUp()
        {
            _mockReportRequestBuilder = new Mock<IReportRequestBuilder>();
            _mockServiceProvider = new Mock<IServiceLocator>();
            _mockServiceProvider.Setup(m => m.GetService<IReportRequestBuilder>())
                                .Returns(_mockReportRequestBuilder.Object);
            GlobalConfig.ServiceLocator = _mockServiceProvider.Object;

            _subject = new RemoteErrorReportStorage();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceLocator = null;
        }

        [Test]
        public void Ctor_Always_CallsServiceProviderGetInstanceOfTypeIReportRequestBuilder()
        {
            _mockServiceProvider.Verify(m => m.GetService<IReportRequestBuilder>());
        }

        [Test]
        public void TryStore_Always_CallsReportRequestBuilderBuildWithReport()
        {
            var errorReport = new ErrorReport();

            new SequentialResult(_subject.Save(errorReport)).Execute(new ExecutionContext());

            _mockReportRequestBuilder.Verify(m => m.BuildAsync(errorReport));
        }

        [Test]
        public void TryStore_WhenNoException_ReturnsTrue()
        {
            var errorReport = new ErrorReport();
            var mockRequest = new Mock<WebRequest>();
            _mockReportRequestBuilder.Setup(m => m.BuildAsync(errorReport))
                .Raises(m => m.BuildCompleted += null, new ReportRequestBuildCompletedEventArgs { Result = mockRequest.Object });
            ErrorReportSaveCompletedEventArgs eventArgs = null;
            _subject.SaveCompleted += (o, e) => { eventArgs = e; };

            _subject.Save(errorReport).ToList();

            Assert.IsTrue(eventArgs.Success);
        }
    }
}
