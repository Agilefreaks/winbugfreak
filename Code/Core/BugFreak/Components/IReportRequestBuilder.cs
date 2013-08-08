using System;

namespace BugFreak.Components
{
    public interface IReportRequestBuilder
    {
        event EventHandler<ReportRequestBuildCompletedEventArgs> BuildCompleted; 

        void BuildAsync(ErrorReport report);
    }
}