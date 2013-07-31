using System;
using System.IO;
using System.Net;
using BugFreak;
using BugFreak.Components;
using BugFreak.Framework;
using BugFreak.Results;
using Moq;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class ReportRequestBuilderTests
    {
        const string SERIALIZER_OUTPUT = "serializer output";

        private Mock<IServiceLocator> _mockServiceProvider;
        private Mock<IErrorReportSerializer> _mockSerializer;
        private Mock<IWebRequestCreate> _mockWebRequestFactory;
        private Mock<WebRequest> _mockWebRequest;
        private MemoryStream _stream;
        private ReportRequestBuilder _subject;

        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Settings.Token = "user-token";
            GlobalConfig.Settings.AppName = "appName";
            GlobalConfig.Settings.ServiceEndPoint = "http://global-endpoint.com";

            _mockSerializer = new Mock<IErrorReportSerializer>();
            _mockWebRequest = new Mock<WebRequest>();
            _mockWebRequest.SetupGet(m => m.Headers).Returns(new WebHeaderCollection());
            _stream = new MemoryStream();
            _mockWebRequest.Setup(m => m.GetRequestStream()).Returns(_stream);
            _mockWebRequestFactory = new Mock<IWebRequestCreate>();
            _mockWebRequestFactory.Setup(m => m.Create(It.IsAny<Uri>()))
                .Returns(_mockWebRequest.Object);
            _mockServiceProvider = new Mock<IServiceLocator>();
            GlobalConfig.ServiceLocator = _mockServiceProvider.Object;
            _mockServiceProvider.Setup(m => m.GetService<IErrorReportSerializer>())
                                .Returns(_mockSerializer.Object);
            _mockServiceProvider.Setup(m => m.GetService<IWebRequestCreate>())
                                .Returns(_mockWebRequestFactory.Object);

            _mockSerializer.Setup(m => m.Serialize(It.IsAny<ErrorReport>()))
                           .Returns(SERIALIZER_OUTPUT);

            _subject = new ReportRequestBuilder();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.Settings.Token = null;
            GlobalConfig.Settings.AppName = null;
            GlobalConfig.Settings.ServiceEndPoint = null;
        }

        [Test]
        public void Build_Always_SetsUriToEndPoint()
        {
            GlobalConfig.Settings.ServiceEndPoint = "http://endpoint.com";

            new SequentialResult(new[] { new RequestBuildResult(_subject, new ErrorReport()) }).Execute(new ExecutionContext());

            _mockWebRequestFactory.Verify(m => m.Create(It.Is<Uri>(u => u.OriginalString == "http://endpoint.com")));
        }

        [Test]
        public void Build_Always_SetsMethodToPost()
        {
            new SequentialResult(new[] { new RequestBuildResult(_subject, new ErrorReport()) }).Execute(new ExecutionContext());

            _mockWebRequest.VerifySet(m => m.Method, "POST");
        }

        [Test]
        public void Build_Always_SetsInstanceIdentifier()
        {
            GlobalConfig.Settings.Token = "user-token";
            var webHeaderCollection = new WebHeaderCollection();
            _mockWebRequest.Setup(m => m.Headers).Returns(webHeaderCollection);

            new SequentialResult(new[] { new RequestBuildResult(_subject, new ErrorReport()) }).Execute(new ExecutionContext());

            Assert.AreEqual("user-token", webHeaderCollection["Token"]);
        }

        [Test]
        public void Build_Always_SetsApiKeyInHeaders()
        {
            GlobalConfig.Settings.ApiKey = "apiKey";
            var webHeaderCollection = new WebHeaderCollection();
            _mockWebRequest.Setup(m => m.Headers).Returns(webHeaderCollection);

            new SequentialResult(new[] { new RequestBuildResult(_subject, new ErrorReport()) }).Execute(new ExecutionContext());

            Assert.AreEqual("apiKey", webHeaderCollection["Api-Key"]);
        }

        [Test]
        public void Build_Always_WritesContentToStream()
        {
            var memoryStream = new MemoryStream();
            var errorReport = new ErrorReport();
            _mockSerializer.Setup(m => m.Serialize(errorReport))
                           .Returns(SERIALIZER_OUTPUT);
            _mockWebRequest.Setup(m => m.EndGetRequestStream(It.IsAny<IAsyncResult>()))
                           .Returns(memoryStream);
            
            new SequentialResult(new[] { new RequestBuildResult(_subject, errorReport) }).Execute(new ExecutionContext());

            _mockWebRequest.Verify(m => m.BeginGetRequestStream(It.IsAny<AsyncCallback>(), It.IsAny<object>()));
        }

        [Test]
        public void Build_Always_CallsSerializerSerialize()
        {
            new SequentialResult(new[] { new RequestBuildResult(_subject, new ErrorReport()) }).Execute(new ExecutionContext());

            _mockSerializer.Verify(m => m.Serialize(It.IsAny<ErrorReport>()));
        }

        [Test]
        public void Build_Always_SetsContentType()
        {
            _mockSerializer.Setup(m => m.GetContentType()).Returns("contentType");

            new SequentialResult(new[] { new RequestBuildResult(_subject, new ErrorReport()) }).Execute(new ExecutionContext());

            _mockWebRequest.VerifySet(m => m.ContentType, "contentType");
        }
    }
}
