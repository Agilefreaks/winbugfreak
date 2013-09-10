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
        private Mock<IServiceLocator> _mockServiceProvider;
        private Mock<IErrorHandler> _mockErrorReportHandler;

        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Token = "user-token";
            GlobalConfig.ServiceEndPoint = "http://myTests.com";
            GlobalConfig.ApiKey = "apiKey";

            ReportingService.Init();

            _mockErrorReportHandler = new Mock<IErrorHandler>();
            _mockServiceProvider = new Mock<IServiceLocator>();
            _mockServiceProvider.Setup(m => m.GetService<IErrorHandler>())
                                .Returns(_mockErrorReportHandler.Object);
            
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
            GlobalConfig.Token = null;
            GlobalConfig.ApiKey = null;
            GlobalConfig.ServiceEndPoint = null;
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
            GlobalConfig.Token = null;

            ReportingService.Init();
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Init_WhenApiKeyNotSet_RaisesArgumentException()
        {
            GlobalConfig.ApiKey = null;

            ReportingService.Init();
        }

        [Test]
        public void Init_WhenInvalidServiceEndpoint_DoesNotRaiseException()
        {
            GlobalConfig.ServiceEndPoint = "http:/test.com";

            ReportingService.Init();

            Assert.Pass();
        }

        [Test]
        public void Init_Always_SetsSerializerToFormSerializer()
        {
            GlobalConfig.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ErrorReportSerializer is FormErrorReportSerializer);
        }

        [Test]
        public void Init_Always_SetsDefaultIRemoteErrorReportStorage()
        {
            GlobalConfig.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorReportStorage>() is RemoteErrorReportStorage);
        }

        [Test]
        public void Init_Always_SetsDefaultErrorHandler()
        {
            GlobalConfig.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IErrorHandler>() is ErrorHandler);
        }

        [Test]
        public void Init_Always_SetsDefaultWebRequestCreate()
        {
            GlobalConfig.Token = "user-token";

            ReportingService.Init();

            Assert.IsTrue(GlobalConfig.ServiceLocator.GetService<IWebRequestCreate>() is WebRequestFactory);
        }

        [Test]
        public void BeginReport_Always_CallsHandlerHandle()
        {
            var exception = new Exception();

            ReportingService.Instance.BeginReport(exception);

            _mockErrorReportHandler.Verify(m => m.Handle(exception, It.IsAny<ReportCompletedCallback>()));
        }

        [Test]
        public void BeginReport_Always_PassesTheCallBack()
        {
            var exception = new Exception();
            ReportCompletedCallback callback = (ex, repored) => { };

            ReportingService.Instance.BeginReport(exception, callback);

            _mockErrorReportHandler.Verify(m => m.Handle(exception, callback));
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

            _mockErrorReportHandler.Verify(m => m.Dispose());
        }

        [Test]
        public void Init_WhenServiceEndpointIsNotSet_SetsDefault()
        {
            GlobalConfig.Token = "token";
            GlobalConfig.ApiKey = "apikey";
            GlobalConfig.ServiceEndPoint = null;

            ReportingService.Init();

            Assert.AreEqual("https://www.bugfreak.co/v1/api/errors", GlobalConfig.ServiceEndPoint);
        }

        [Test]
        public void Init_WhenServiceEndpointIsSet_DoesNotOverwrite()
        {
            GlobalConfig.Token = "token";
            GlobalConfig.ApiKey = "apikey";
            GlobalConfig.ServiceEndPoint = "http://test.com";

            ReportingService.Init();

            Assert.AreEqual("http://test.com", GlobalConfig.ServiceEndPoint);
        }
    }
}
