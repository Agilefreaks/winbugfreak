using System;
using System.Net;

namespace BugFreak.Results
{
    using global::BugFreak.Components;
    using global::BugFreak.Framework;

    public class RequestBuildResult : IResult
    {
        private IReportRequestBuilder _builder;
        private ErrorReport _report;

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public WebRequest Result { get; set; }

        public RequestBuildResult(IReportRequestBuilder builder, ErrorReport report)
        {
            _builder = builder;
            _report = report;
        }

        public void Execute(ExecutionContext context)
        {
            _builder.BuildCompleted += BuilderOnBuildCompleted;
            try
            {
                _builder.BuildAsync(_report);
            }
            catch (Exception exception)
            {
                OnCompleted(new ResultCompletionEventArgs { WasCancelled = true, Error = exception });
            }
        }

        private void BuilderOnBuildCompleted(object sender, ReportRequestBuildCompletedEventArgs eventArgs)
        {
            Result = eventArgs.Result;
            var resultCompletionEventArgs = new ResultCompletionEventArgs();
            if (eventArgs.Error != null)
            {
                resultCompletionEventArgs.WasCancelled = true;
                resultCompletionEventArgs.Error = eventArgs.Error;
            }
            
            OnCompleted(resultCompletionEventArgs);
        }

        protected virtual void OnCompleted(ResultCompletionEventArgs e)
        {
            _builder.BuildCompleted -= BuilderOnBuildCompleted;
            _builder = null;
            _report = null;

            var handler = Completed;
            if (handler != null)
            {
                handler(this, e);
            }

            Completed = null;
        }
    }
}
