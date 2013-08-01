using System;
using System.Net;
using BugFreak;
using BugFreak.Components;
using BugFreak.Framework;
using Moq;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class AgileReporterTests
    {
        private Mock<IErrorReportQueue> _mockErrorQueue;
        private Mock<IServiceLocator> _mockServiceProvider;
        private Mock<IErrorReportHandler> _mockErrorReportHandler;
        private Mock<IErrorReportQueueListener> _mockErrorReportQueueListener;

        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Settings.Token = "user-token";
            GlobalConfig.Settings.ServiceEndPoint = "http://myTests.com";
            GlobalConfig.Settings.ApiKey = "apiKey";

            AgileReporter.Init();

            _mockErrorQueue = new Mock<IErrorReportQueue>();
            _mockErrorReportHandler = new Mock<IErrorReportHandler>();
            _mockErrorReportQueueListener = new Mock<IErrorReportQueueListener>();
            _mockServiceProvider = new Mock<IServiceLocator>();
            _mockServiceProvider.Setup(m => m.GetService<IErrorReportQueue>())
                                .Returns(_mockErrorQueue.Object);
            _mockServiceProvider.Setup(m => m.GetService<IErrorReportHandler>())
                                .Returns(_mockErrorReportHandler.Object);
            _mockServiceProvider.Setup(m => m.GetService<IErrorReportQueueListener>())
                                .Returns(_mockErrorReportQueueListener.Object);

            GlobalConfig.ServiceLocator = _mockServiceProvider.Object;
        }

        [TearDown]
        public void TearDown()
        {
            if (AgileReporter.Instance != null)
            {
                AgileReporter.Dispose();
            }

            GlobalConfig.ServiceLocator = null;
            GlobalConfig.Settings.Token = null;
            GlobalConfig.Settings.ApiKey = null;
            GlobalConfig.Settings.ServiceEndPoint = null;
        }

        [Test]
        public void Instance_Always_ReturnsNotNull()
        {
            var instance = AgileReporter.Instance;

            Assert.IsNotNull(instance);
        }

        [Test]
        public void Instance_Always_ReturnsSameInstance()
        {
            var instance1 = AgileReporter.Instance;
            var instance2 = AgileReporter.Instance;

            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void Instance_AfterCallingDisposeOnCurrentInstance_ReturnsNull()
        {
            AgileReporter.Dispose();

            var instance = AgileReporter.Instance;

            Assert.IsNull(instance);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Init_WhenApiKeyIsNotSet_RaisesArgumentException()
        {
            GlobalConfig.Settings.Token = null;

            AgileReporter.Init();
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Init_WhenApiKeyNotSet_RaisesArgumentException()
        {
            GlobalConfig.Settings.ApiKey = null;

            AgileReporter.Init();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Init_WhenInvalidServiceEndpoint_RaisesArgumentException()
        {
            GlobalConfig.Settings.ServiceEndPoint = "http:/test.com";

            AgileReporter.Init();
        }

        [Test]
        public void Init_Always_SetsSerializerToFormSerializer()
        {
            GlobalConfig.Settings.Token = "user-token";

            AgileReporter.Init();

            Assert.IsTrue(GlobalConfig.ErrorReportSerializer is FormErrorReportSerializer);
        }

        [Test]
        public void Init_Always_SetsDefaultIRemoteErrorReportStorage()
        {
            GlobalConfig.Settings.Token = "user-token";

            AgileReporter.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportStorage>() is RemoteErrorReportStorage);
        }

        [Test]
        public void Init_Always_SetsDefaultErrorQueueListener()
        {
            GlobalConfig.Settings.Token = "user-token";

            AgileReporter.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportQueueListener>() is ErrorReportQueueListener);
        }

        [Test]
        public void Init_Always_SetsDefaultErrorHandler()
        {
            GlobalConfig.Settings.Token = "user-token";

            AgileReporter.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportHandler>() is ErrorReportHandler);
        }

        [Test]
        public void Init_Always_SetsDefaultWebRequestCreate()
        {
            GlobalConfig.Settings.Token = "user-token";

            AgileReporter.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IWebRequestCreate>() is WebRequestFactory);
        }

        [Test]
        public void BeginRequest_Always_CallsReportQueueEnqueue()
        {
            AgileReporter.Instance.BeginReport(new Exception());

            _mockErrorQueue.Verify(m => m.Enqueue(It.IsAny<ErrorReport>()));
        }

        [Test]
        public void Dispose_Always_CallsHandlerDispose()
        {
            AgileReporter.Dispose();

            _mockErrorReportHandler.Verify(m => m.Dispose());
        }

        [Test]
        public void Dispose_Always_CallsListenerDispose()
        {
            AgileReporter.Dispose();

            _mockErrorReportQueueListener.Verify(m => m.Dispose());
        }
    }
}
