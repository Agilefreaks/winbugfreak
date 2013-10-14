using System.Globalization;
using System.Linq;
using BugFreak.Core.Components;
using FluentAssertions;
using NUnit.Framework;

namespace BugFreak.MVC3.Tests.Components
{
    [TestFixture]
    public class SessionErrorDataProviderTests
    {
        private SessionErrorDataProvider _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new SessionErrorDataProvider();
        }

        [Test]
        public void GetData_Always_SetsSessionId()
        {
            _subject.HttpContext = FakeHttpContext.Create("Report");

            var result = _subject.GetData();

            var sessionId = result.First(pair => pair.Key == "SessionID").Value;
            sessionId.Should().Be(_subject.HttpContext.Session.SessionID);
        }

        [Test]
        public void GetData_Always_SetsIsNewFlag()
        {
            _subject.HttpContext = FakeHttpContext.Create("Report");

            var result = _subject.GetData();

            var isNew = result.First(pair => pair.Key == "SessionIsNew").Value;
            isNew.Should().Be(_subject.HttpContext.Session.IsNewSession.ToString());
        }

        [Test]
        public void GetData_Always_SetsSessionTimeout()
        {
            _subject.HttpContext = FakeHttpContext.Create("Report");

            var result = _subject.GetData();

            var timeout = result.First(pair => pair.Key == "SessionTimeout").Value;
            timeout.Should().Be(_subject.HttpContext.Session.Timeout.ToString(CultureInfo.InvariantCulture));
        }
    }
}