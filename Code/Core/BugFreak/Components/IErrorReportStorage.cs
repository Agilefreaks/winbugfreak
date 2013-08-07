using System;

namespace Bugfreak.Components
{
    public interface IErrorReportStorage
    {
        event EventHandler<ErrorReportSaveCompletedEventArgs> SaveCompleted;

        void SaveAsync(ErrorReport report);
    }
}