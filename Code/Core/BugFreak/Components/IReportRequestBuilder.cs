using System;

namespace Bugfreak.Components
{
    public interface IReportRequestBuilder
    {
        event EventHandler<ReportRequestBuildCompletedEventArgs> BuildCompleted; 

        void BuildAsync(ErrorReport report);
    }
}