using System;
using BugFreak;
using BugFreak.Components;
using Moq;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class ErrorReportQueueListenerTests
    {
        private Mock<IServiceProvider> _mockServiceProvider;
        private Mock<IErrorReportHandler> _mockErrorReportHandler;
        private ErrorReportQueueListener _subject;

        [SetUp]
        public void SetUp()
        {
            _mockErrorReportHandler = new Mock<IErrorReportHandler>();
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockServiceProvider.Setup(m => m.GetService(typeof(IErrorReportHandler)))
                                .Returns(_mockErrorReportHandler.Object);

            GlobalConfig.ServiceProvider = _mockServiceProvider.Object;

            _subject = new ErrorReportQueueListener();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceProvider = null;
        }

        [Test]
        public void Listen_Always_CallsServiceProviderGetServiceOfTypeIErrorReportHandler()
        {
            _subject.Listen(new ErrorReportQueue());

            _mockServiceProvider.Verify(m => m.GetService(typeof(IErrorReportHandler)));
        }

        [Test]
        public void QueueAddedItem_RemovesItemFromQueue()
        {
            var errorReport = new ErrorReport();
            var list = new ErrorReportQueue();
            _subject.Listen(list);
            
            list.Add(errorReport);

            CollectionAssert.DoesNotContain(list, errorReport);
        }

        [Test]
        public void QueueAddedItem_CallsErrorReportHandlerHandle()
        {
            var errorReport = new ErrorReport();
            var list = new ErrorReportQueue();
            _subject.Listen(list);
            
            list.Add(errorReport);

            _mockErrorReportHandler.Verify(m => m.Handle(errorReport));
        }

        [Test]
        public void Dispose_Always_CallsReportHandlerDispose()
        {
            var list = new ErrorReportQueue();
            _subject.Listen(list);

            _subject.Dispose();

            _mockErrorReportHandler.Verify(m => m.Dispose());
        }
    }
}