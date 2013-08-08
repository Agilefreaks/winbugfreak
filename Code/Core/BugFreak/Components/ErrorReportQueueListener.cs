using BugFreak.Collections;
using BugFreak.Framework;

namespace BugFreak.Components
{
    using global::BugFreak.Collections;
    using global::BugFreak.Framework;

    public class ErrorReportQueueListener : IErrorReportQueueListener
    {
        private IErrorReportQueue _queue;
        private IErrorReportHandler _errorReportHandler;

        public void Listen(IErrorReportQueue reportQueue)
        {
            _errorReportHandler = GlobalConfig.ServiceLocator.GetService<IErrorReportHandler>();

            _queue = reportQueue;
            _queue.ItemAdded += QueueOnItemAdded;
        }

        public void Dispose()
        {
            _queue.ItemAdded -= QueueOnItemAdded;
            _queue = null;
            _errorReportHandler.Dispose();
            _errorReportHandler = null;
        }

        private void QueueOnItemAdded(object sender, ObservableList<ErrorReport>.ListChangedEventArgs listChangedEventArgs)
        {
            BeginWork();
        }

        private void BeginWork()
        {
            var item = _queue.Dequeue();

            var result = new SequentialResult(_errorReportHandler.Handle(item));
            result.Execute(new ExecutionContext());
        }
    }
}