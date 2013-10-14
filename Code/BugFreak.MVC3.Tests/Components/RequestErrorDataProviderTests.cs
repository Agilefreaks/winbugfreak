using System.Globalization;
using System.Linq;
using BugFreak.Core.Components;
using FluentAssertions;
using NUnit.Framework;

namespace BugFreak.MVC3.Tests.Components
{
    [TestFixture]
    public class RequestErrorDataProviderTests
    {
        private RequestErrorDataProvider _subject;
        
        [SetUp]
        public void SetUp()
        {
            _subject = new RequestErrorDataProvider();
        }

        [Test]
        public void GetData_Always_SetsRequestTimeStamp()
        {
            _subject.HttpContext = FakeHttpContext.Create("Report");
            
            var result = _subject.GetData();

            var timeStamp = result.First(pair => pair.Key == "Timestamp").Value;
            timeStamp.Should().Be(_subject.HttpContext.Timestamp.ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void GetData_Always_SetsAbsoluteUri()
        {
            _subject.HttpContext = FakeHttpContext.Create("Report");

            var result = _subject.GetData();

            var absoluteUri = result.First(pair => pair.Key == "URL").Value;
            absoluteUri.Should().Be(_subject.HttpContext.Request.Url.AbsoluteUri);
        }

        [Test]
        public void GetData_Always_SetsQueryString()
        {
            const string query = "key1=val1&key2=val2";
            _subject.HttpContext = FakeHttpContext.Create("Report", query);

            var result = _subject.GetData();

            var addedQuery = result.First(pair => pair.Key == "Query").Value;
            addedQuery.Should().Be(query);
        }
    }
}
