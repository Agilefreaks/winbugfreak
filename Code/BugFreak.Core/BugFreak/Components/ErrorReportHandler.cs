using System;
using System.ComponentModel;
using BugFreak.Utils;

namespace BugFreak.Components
{
    public class ErrorReportHandler : IErrorReportHandler
    {
        private readonly IRemoteErrorReportStorage _remoteStorage;
        private readonly ILocalErrorReportStorage _localStorage;
        private BackgroundWorker _worker;

        public event EventHandler HandleComplete;

        public ErrorReportHandler()
        {
            _remoteStorage = GlobalConfig.ServiceProvider.GetService<IRemoteErrorReportStorage>();
            _localStorage = GlobalConfig.ServiceProvider.GetService<ILocalErrorReportStorage>();
        }

        public void Handle(ErrorReport errorReport)
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_WorkCompleted;
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerAsync(errorReport);
        }

        public void Dispose()
        {
            if (_worker != null && _worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }

        protected virtual void OnHandleComplete()
        {
            var handler = HandleComplete;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var errorReport = (ErrorReport)doWorkEventArgs.Argument;

            var stored = _remoteStorage.TryStore(errorReport);

            if (!stored)
            {
                _localStorage.TryStore(errorReport);
            }
        }

        private void Worker_WorkCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            OnHandleComplete();
        }
    }
}
