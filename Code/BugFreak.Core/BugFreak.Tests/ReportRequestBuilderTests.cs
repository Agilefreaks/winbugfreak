using System;
using System.IO;
using System.Net;
using BugFreak;
using BugFreak.Components;
using Moq;
using NUnit.Framework;

namespace AgileBug.Tests
{
    [TestFixture]
    public class ReportRequestBuilderTests
    {
        const string SERIALIZER_OUTPUT = "serializer output";

        private Mock<IServiceProvider> _mockServiceProvider;
        private Mock<IErrorReportSerializer> _mockSerializer;
        private Mock<IWebRequestCreate> _mockWebRequestFactory;
        private Mock<WebRequest> _mockWebRequest;
        private MemoryStream _stream;
        private ReportRequestBuilder _subject;

        [SetUp]
        public void SetUp()
        {
            GlobalConfig.Settings.InstanceIdentifier = "user-token";
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
            _mockServiceProvider = new Mock<IServiceProvider>();
            GlobalConfig.ServiceProvider = _mockServiceProvider.Object;
            _mockServiceProvider.Setup(m => m.GetService(typeof(IErrorReportSerializer)))
                                .Returns(_mockSerializer.Object);
            _mockServiceProvider.Setup(m => m.GetService(typeof(IWebRequestCreate)))
                                .Returns(_mockWebRequestFactory.Object);

            _mockSerializer.Setup(m => m.Serialize(It.IsAny<ErrorReport>()))
                           .Returns(SERIALIZER_OUTPUT);

            _subject = new ReportRequestBuilder();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalConfig.Settings.InstanceIdentifier = null;
            GlobalConfig.Settings.AppName = null;
            GlobalConfig.Settings.ServiceEndPoint = null;
        }

        [Test]
        public void Build_Always_SetsUriToEndPoint()
        {
            GlobalConfig.Settings.ServiceEndPoint = "http://endpoint.com";

            var result = _subject.Build(new ErrorReport());
            result.Abort();

            _mockWebRequestFactory.Verify(m => m.Create(It.Is<Uri>(u => u.OriginalString == "http://endpoint.com")));
        }

        [Test]
        public void Build_Always_SetsMethodToPost()
        {
            var result = _subject.Build(new ErrorReport());
            result.Abort();

            _mockWebRequest.VerifySet(m => m.Method, "POST");
        }

        [Test]
        public void Build_Always_SetsInstanceIdentifier()
        {
            GlobalConfig.Settings.InstanceIdentifier = "user-token";

            var result = _subject.Build(new ErrorReport());
            result.Abort();

            Assert.AreEqual(GlobalConfig.Settings.InstanceIdentifier, result.Headers["InstanceIdentifier"]);
        }

        [Test]
        public void Build_Always_SetsApiKeyInHeaders()
        {
            GlobalConfig.Settings.ApiKey = "apiKey";

            var result = _subject.Build(new ErrorReport());
            result.Abort();

            Assert.AreEqual(GlobalConfig.Settings.ApiKey, result.Headers["apiKey"]);
        }

        [Test]
        public void Build_Always_WritesContentToStream()
        {
            var errorReport = new ErrorReport();
            _mockSerializer.Setup(m => m.Serialize(errorReport))
                           .Returns(SERIALIZER_OUTPUT);

            var result = _subject.Build(errorReport);
            result.Abort();

            Assert.AreEqual(SERIALIZER_OUTPUT, new StreamReader(_stream).ReadToEnd());
        }

        [Test]
        public void Build_Always_CallsSerializerSerialize()
        {
            var result = _subject.Build(new ErrorReport());
            result.Abort();

            _mockSerializer.Verify(m => m.Serialize(It.IsAny<ErrorReport>()));
        }

        [Test]
        public void Build_Always_SetsContentType()
        {
            _mockSerializer.Setup(m => m.GetContentType()).Returns("contentType");

            var resut = _subject.Build(new ErrorReport());
            resut.Abort();

            _mockWebRequest.VerifySet(m => m.ContentType, "contentType");
        }
    }
}
