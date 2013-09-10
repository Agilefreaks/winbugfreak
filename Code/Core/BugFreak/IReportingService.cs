namespace BugFreak
{
    using System;

    public delegate void ReportCompletedCallback(Exception exc, bool reported);

    public interface IReportingService
    {
        void BeginReport(Exception exc);

        void BeginReport(Exception exc, ReportCompletedCallback reportCompletedCallback);
    }
}