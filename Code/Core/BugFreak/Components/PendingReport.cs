namespace BugFreak.Components
{
    using System;

    public class PendingReport
    {
        public Exception Exception { get; private set; }

        public ReportCompletedCallback ReportCompletedCallback { get; private set; }

        public PendingReport(Exception exception, ReportCompletedCallback reportCompletedCallback)
        {
            Exception = exception;
            ReportCompletedCallback = reportCompletedCallback;
        }
    }
}