using System;
using System.Threading;
using BugFreak.Collections;
using BugFreak.Utils;

namespace BugFreak.Components
{
    public class ErrorReportQueueListener : IErrorReportQueueListener
    {
        private IErrorReportQueue _queue;
        private IErrorReportHandler _errorReportHandler;
        
        public void Listen(IErrorReportQueue reportQueue)
        {
            _errorReportHandler = GlobalConfig.ServiceProvider.GetService<IErrorReportHandler>();

            _queue = reportQueue;
            _queue.ItemAdded += QueueOnItemAdded;
            _errorReportHandler.HandleComplete += WorkCompleted;
        }

        public void Dispose()
        {
            _errorReportHandler.HandleComplete -= WorkCompleted;
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
            Lock();

            var item = _queue.Dequeue();
            _errorReportHandler.Handle(item);
        }

        private void WorkCompleted(object sender, EventArgs eventArgs)
        {
            Unlock();
        }

        private void Lock()
        {
            Monitor.Enter(_queue);
        }

        private void Unlock()
        {
            Monitor.Exit(_queue);
        }
    }
}