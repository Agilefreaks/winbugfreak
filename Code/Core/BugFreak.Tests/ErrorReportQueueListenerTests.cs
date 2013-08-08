﻿using BugFreak;
using BugFreak.Components;
using BugFreak.Framework;
using Moq;
using NUnit.Framework;

namespace BugFreak.Tests
{
    [TestFixture]
    public class ErrorReportQueueListenerTests
    {
        private Mock<IServiceLocator> _mockServiceProvider;
        private Mock<IErrorReportHandler> _mockErrorReportHandler;
        private ErrorReportQueueListener _subject;

        [SetUp]
        public void SetUp()
        {
            _mockErrorReportHandler = new Mock<IErrorReportHandler>();
            _mockServiceProvider = new Mock<IServiceLocator>();
            _mockServiceProvider.Setup(m => m.GetService<IErrorReportHandler>())
                                .Returns(_mockErrorReportHandler.Object);

            GlobalConfig.ServiceLocator = _mockServiceProvider.Object;

            _subject = new ErrorReportQueueListener();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.ServiceLocator = null;
        }

        [Test]
        public void Listen_Always_CallsServiceProviderGetServiceOfTypeIErrorReportHandler()
        {
            _subject.Listen(new ErrorReportQueue());

            _mockServiceProvider.Verify(m => m.GetService<IErrorReportHandler>());
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