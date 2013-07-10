using System;

namespace BugFreak.Components
{
    public interface IErrorReportStorage
    {
        event EventHandler<ErrorReportSaveCompletedEventArgs> SaveCompleted;

        void SaveAsync(ErrorReport report);
    }
}