namespace BugFreak.Results
{
    using System;
    using global::BugFreak.Components;
    using global::BugFreak.Framework;

    public class ErrorReportSaveResult : IResult
    {
        private ErrorReport _report;
        private IErrorReportStorage _storage;
        public event EventHandler<ResultCompletionEventArgs> Completed;

        public ErrorReportSaveResult(IErrorReportStorage storage, ErrorReport report)
        {
            _storage = storage;
            _report = report;
        }

        public void Execute(ExecutionContext context)
        {
            _storage.SaveCompleted += StorageOnSaveCompleted;
            _storage.SaveAsync(_report);
        }

        private void StorageOnSaveCompleted(object sender, ErrorReportSaveCompletedEventArgs eventArgs)
        {
            OnCompleted(new ResultCompletionEventArgs { WasCancelled = eventArgs.Success });
        }

        protected virtual void OnCompleted(ResultCompletionEventArgs e)
        {
            _storage.SaveCompleted -= StorageOnSaveCompleted;
            _storage = null;
            _report = null;

            var handler = Completed;
            if (handler != null)
            {
                handler(this, e);
            }

            Completed = null;
        }
    }
}