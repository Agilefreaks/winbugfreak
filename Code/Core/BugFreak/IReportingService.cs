using System;

namespace Bugfreak
{
    public interface IReportingService
    {
        void BeginReport(Exception exc);
    }
}