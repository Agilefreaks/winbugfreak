using System.Linq;
using System.Security.Principal;
using System.Web;
using BugFreak.Core.Components;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BugFreak.MVC3.Tests.Components
{
    [TestFixture]
    public class UserErrorDataProviderTests
    {
        private UserErrorDataProvider _subject;
        private Mock<HttpContextBase> _mockHttpContext;
        private Mock<HttpRequestBase> _mockRequest;
        private Mock<HttpSessionStateBase> _mockSessionState;
        private Mock<IPrincipal> _mockPrincipal;
        private Mock<IIdentity> _mockIdentity;

        [SetUp]
        public void SetUp()
        {
            _mockHttpContext = new Mock<HttpContextBase>();
            _mockRequest = new Mock<HttpRequestBase>();
            _mockSessionState = new Mock<HttpSessionStateBase>();
            _mockPrincipal = new Mock<IPrincipal>();
            _mockIdentity = new Mock<IIdentity>();
            _mockHttpContext.SetupGet(m => m.Request).Returns(_mockRequest.Object);
            _mockHttpContext.SetupGet(m => m.Session).Returns(_mockSessionState.Object);
            _mockHttpContext.SetupGet(m => m.User).Returns(_mockPrincipal.Object);
            _mockPrincipal.SetupGet(m => m.Identity).Returns(_mockIdentity.Object);

            _subject = new UserErrorDataProvider
                {
                    HttpContext = _mockHttpContext.Object
                };
        }

        [Test]
        public void GetData_WhenIsAuthenticated_AddsItemsToResult()
        {
            _mockRequest.SetupGet(m => m.IsAuthenticated).Returns(true);

            var result = _subject.GetData();

            result.Should().HaveCount(c => c > 0);
        }

        [Test]
        public void GetData_WhenIsNotAuthenticated_DoesNotAddItemsToResult()
        {
            _mockRequest.SetupGet(m => m.IsAuthenticated).Returns(false);

            var result = _subject.GetData();

            result.Should().HaveCount(c => c == 0);
        }

        [Test]
        public void GetData_WhenIsAuthenticated_AddsUsername()
        {
            const string username = "testUsername";
            _mockRequest.SetupGet(m => m.IsAuthenticated).Returns(true);
            _mockIdentity.SetupGet(m => m.Name).Returns(username);

            var result = _subject.GetData();

            var addedUsername = result.First(pair => pair.Key == "Username").Value;
            addedUsername.Should().Be(username);
        }

        [Test]
        public void GetData_WhenIsAuthenticated_AddsAuthenticationType()
        {
            const string authenticationType = "Forms";
            _mockIdentity.SetupGet(m => m.AuthenticationType).Returns(authenticationType);
            _mockRequest.SetupGet(m => m.IsAuthenticated).Returns(true);
            _mockIdentity.SetupGet(m => m.AuthenticationType).Returns(authenticationType);

            var result = _subject.GetData();

            var addedAuthenticationType = result.First(pair => pair.Key == "AuthenticationType");
            addedAuthenticationType.Should().Be(addedAuthenticationType);
        }
    }
}
