namespace BugFreak.Components
{
    using System;
    using System.Linq;
    using Collections;
    using Framework;
    using Results;

    public class ErrorHandler : IErrorHandler
    {
        private IErrorQueue _errorQueue;

        public ErrorHandler()
        {
            _errorQueue = GlobalConfig.ServiceLocator.GetService<IErrorQueue>();
            _errorQueue.ItemAdded += OnNewError;
        }

        public void Handle(Exception exception, ReportCompletedCallback callback)
        {
            _errorQueue.Enqueue(new PendingReport(exception, callback));
        }

        public void Dispose()
        {
            _errorQueue = null;
        }

        private void OnNewError(object sender,ObservableList<PendingReport>.ListChangedEventArgs e)
        {
            var item = _errorQueue.Dequeue();
            var report = ErrorReport.FromException(item.Exception);

            var storageLocations = GlobalConfig.ServiceLocator.GetServices<IErrorReportStorage>();
            var result = new SequentialResult(storageLocations.Select(storage => new ErrorReportSaveResult(storage, report)).Cast<IResult>());
            result.Completed += (o, args) => OnReportingCompleted(e, args);
            result.Execute(new ExecutionContext());
        }

        private void OnReportingCompleted(ObservableList<PendingReport>.ListChangedEventArgs e, ResultCompletionEventArgs args)
        {
            if (e.Item.ReportCompletedCallback != null)
            {
                e.Item.ReportCompletedCallback(e.Item.Exception, args.WasCancelled);
            }
        }
    }
}
