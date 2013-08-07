using System;
using System.Collections.Generic;
using Bugfreak.Framework;
using Bugfreak.Results;

namespace Bugfreak.Components
{
    public class RemoteErrorReportStorage : IErrorReportStorage
    {
        private readonly IReportRequestBuilder _reportRequestBuilder;

        public RemoteErrorReportStorage()
        {
            _reportRequestBuilder = GlobalConfig.ServiceLocator.GetService<IReportRequestBuilder>();
        }

        public event EventHandler<ErrorReportSaveCompletedEventArgs> SaveCompleted;

        public void SaveAsync(ErrorReport report)
        {
            new SequentialResult(Save(report)).Execute(new ExecutionContext());
        }

        public IEnumerable<IResult> Save(ErrorReport report)
        {
            var requestBuildResult = new RequestBuildResult(_reportRequestBuilder, report);
            yield return requestBuildResult;

            var requestExecuteResult = new RequestExecutionResult(requestBuildResult.Result);
            yield return requestExecuteResult;

            OnSaveCompleted(new ErrorReportSaveCompletedEventArgs { Success = true });
        }

        protected virtual void OnSaveCompleted(ErrorReportSaveCompletedEventArgs e)
        {
            var handler = SaveCompleted;
            if (handler != null) handler(this, e);
        }
    }
}