using Bugfreak;
using Bugfreak.Components;
using NUnit.Framework;

namespace BugFreak.Tests
{
    [TestFixture]
    public class ErrorReportQueueTests
    {
        private ErrorReportQueue _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new ErrorReportQueue();
        }

        [Test]
        public void Dequeue_Always_RemovesElementFromQueue()
        {
            _subject.Enqueue(new ErrorReport());
            _subject.Enqueue(new ErrorReport());
            _subject.Enqueue(new ErrorReport());

            var item = _subject.Dequeue();

            CollectionAssert.DoesNotContain(_subject, item);
        }

        [Test]
        public void Dequeue_Always_ReturnsFirstElement()
        {
            var firstItem = new ErrorReport();
            _subject.Enqueue(firstItem);
            _subject.Enqueue(new ErrorReport());
            _subject.Enqueue(new ErrorReport());

            var item = _subject.Dequeue();

            Assert.AreSame(firstItem, item);
        }
    }
}
