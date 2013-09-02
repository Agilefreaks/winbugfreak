namespace BugFreak.Tests
{
    using Components;
    using NUnit.Framework;

    [TestFixture]
    public class ErrorReportQueueTests
    {
        private ErrorQueue _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new ErrorQueue();
        }

        [Test]
        public void Dequeue_Always_RemovesElementFromQueue()
        {
            _subject.Enqueue(new PendingReport(null, null));
            _subject.Enqueue(new PendingReport(null, null));
            _subject.Enqueue(new PendingReport(null, null));

            var item = _subject.Dequeue();

            CollectionAssert.DoesNotContain(_subject, item);
        }

        [Test]
        public void Dequeue_Always_ReturnsFirstElement()
        {
            var firstItem = new PendingReport(null, null);
            _subject.Enqueue(firstItem);
            _subject.Enqueue(new PendingReport(null, null));
            _subject.Enqueue(new PendingReport(null, null));

            var item = _subject.Dequeue();

            Assert.AreSame(firstItem, item);
        }
    }
}
