using System;

namespace BugFreak
{
    public interface IReportingService
    {
        void BeginReport(Exception exc);
    }
}