using System;

namespace Bugfreak.Components
{
    public class LocalErrorReportStorage : IErrorReportStorage
    {
        public event EventHandler<ErrorReportSaveCompletedEventArgs> SaveCompleted;

        public void SaveAsync(ErrorReport report)
        {
            OnSaveCompleted(new ErrorReportSaveCompletedEventArgs { Success = true });
        }

        protected virtual void OnSaveCompleted(ErrorReportSaveCompletedEventArgs e)
        {
            var handler = SaveCompleted;
            if (handler != null) handler(this, e);
        }
    }
}