namespace BugFreak.Silverlight.Tests.Components
{
    using global::BugFreak.Components;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SilverlightErrorDataProviderTests
    {
        private SilverlightErrorDataProvider _subject;

        [TestInitialize]
        public void TestInitialize()
        {
            _subject = new SilverlightErrorDataProvider();
        }

        [TestMethod]
        public void GetData_Always_Works()
        {
            Assert.IsNotNull(_subject.GetData());
        }
    }
}