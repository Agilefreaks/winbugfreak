namespace BugFreak.WPF.Tests.Components
{
    using global::BugFreak.Components;

    using NUnit.Framework;

    [TestFixture]
    public class WpfErrorDataProviderTests
    {
        private WpfErrorDataProvider _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new WpfErrorDataProvider();
        }

        [Test]
        public void GetData_Always_ReturnsAList()
        {
            Assert.IsNotNull(_subject.GetData());
        }
    }
}