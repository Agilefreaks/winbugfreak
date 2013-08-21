namespace BugFreak
{
    using System;

    public static class WinRTReportingService
    {
        public static void BeginReport(Exception exc)
        {
            ReportingService.Instance.BeginReport(exc);
        }
    }
}