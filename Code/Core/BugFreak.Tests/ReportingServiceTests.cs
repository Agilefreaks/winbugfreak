namespace BugFreak.Tests
{
    using System;
    using System.Net;
    using BugFreak;
    using BugFreak.Components;
    using BugFreak.Framework;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ReportingServiceTests
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

            ReportingService.Init();

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
            if (ReportingService.Instance != null)
            {
                ReportingService.Dispose();
            }

            GlobalConfig.ServiceLocator = null;
            GlobalConfig.Settings.Token = null;
            GlobalConfig.Settings.ApiKey = null;
            GlobalConfig.Settings.ServiceEndPoint = null;
        }

        [Test]
        public void Instance_Always_ReturnsNotNull()
        {
            var instance = ReportingService.Instance;

            Assert.IsNotNull(instance);
        }

        [Test]
        public void Instance_Always_ReturnsSameInstance()
        {
            var instance1 = ReportingService.Instance;
            var instance2 = ReportingService.Instance;

            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void Instance_AfterCallingDisposeOnCurrentInstance_ReturnsNull()
        {
            ReportingService.Dispose();

            var instance = ReportingService.Instance;

            Assert.IsNull(instance);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Init_WhenApiKeyIsNotSet_RaisesArgumentException()
        {
            GlobalConfig.Settings.Token = null;

            ReportingService.Init();
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Init_WhenApiKeyNotSet_RaisesArgumentException()
        {
            GlobalConfig.Settings.ApiKey = null;

            ReportingService.Init();
        }

        [Test]
        public void Init_WhenInvalidServiceEndpoint_DoesNotRaiseException()
        {
            GlobalConfig.Settings.ServiceEndPoint = "http:/test.com";

            ReportingService.Init();

            Assert.Pass();
        }

        [Test]
        public void Init_Always_SetsSerializerToFormSerializer()
        {
            GlobalConfig.Settings.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ErrorReportSerializer is FormErrorReportSerializer);
        }

        [Test]
        public void Init_Always_SetsDefaultIRemoteErrorReportStorage()
        {
            GlobalConfig.Settings.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportStorage>() is RemoteErrorReportStorage);
        }

        [Test]
        public void Init_Always_SetsDefaultErrorQueueListener()
        {
            GlobalConfig.Settings.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportQueueListener>() is ErrorReportQueueListener);
        }

        [Test]
        public void Init_Always_SetsDefaultErrorHandler()
        {
            GlobalConfig.Settings.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportHandler>() is ErrorReportHandler);
        }

        [Test]
        public void Init_Always_SetsDefaultWebRequestCreate()
        {
            GlobalConfig.Settings.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IWebRequestCreate>() is WebRequestFactory);
        }

        [Test]
        public void BeginRequest_Always_CallsReportQueueEnqueue()
        {
            ReportingService.Instance.BeginReport(new Exception());

            _mockErrorQueue.Verify(m => m.Enqueue(It.IsAny<ErrorReport>()));
        }

        [Test]
        public void Dispose_Always_CallsHandlerDispose()
        {
            ReportingService.Dispose();

            _mockErrorReportHandler.Verify(m => m.Dispose());
        }

        [Test]
        public void Dispose_Always_CallsListenerDispose()
        {
            ReportingService.Dispose();

            _mockErrorReportQueueListener.Verify(m => m.Dispose());
        }

        [Test]
        public void Init_WhenServiceEndpointIsNotSet_SetsDefault()
        {
            GlobalConfig.Settings.Token = "token";
            GlobalConfig.Settings.ApiKey = "apikey";
            GlobalConfig.Settings.ServiceEndPoint = null;

            ReportingService.Init();

            Assert.AreEqual("https://www.bugfreak.co/v1/api/errors", GlobalConfig.Settings.ServiceEndPoint);
        }

        [Test]
        public void Init_WhenServiceEndpointIsSet_DoesNotOverwrite()
        {
            GlobalConfig.Settings.Token = "token";
            GlobalConfig.Settings.ApiKey = "apikey";
            GlobalConfig.Settings.ServiceEndPoint = "http://test.com";

            ReportingService.Init();

            Assert.AreEqual("http://test.com", GlobalConfig.Settings.ServiceEndPoint);
        }
    }
}
