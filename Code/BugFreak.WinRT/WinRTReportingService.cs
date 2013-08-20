using System;

namespace BugFreak.WinRT
{
    public static class WinRTReportingService
    {
        public static void BeginReport(Exception exc)
        {
            ReportingService.Instance.BeginReport(exc);
        }
    }
}