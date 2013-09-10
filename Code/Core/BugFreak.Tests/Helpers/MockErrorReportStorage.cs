namespace BugFreak.Tests.Helpers
{
    using System;
    using BugFreak.Components;

    public class MockErrorReportStorage : IErrorReportStorage
    {
        public event EventHandler<ErrorReportSaveCompletedEventArgs> SaveCompleted;

        public void SaveAsync(ErrorReport report)
        {
            SaveCompleted(this, new ErrorReportSaveCompletedEventArgs());
        }
    }
}
