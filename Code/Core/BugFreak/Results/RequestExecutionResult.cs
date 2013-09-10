using System;
using System.Net;

namespace BugFreak.Results
{
    using global::BugFreak.Framework;

    public class RequestExecutionResult : IResult
    {
        private readonly WebRequest _request;

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public RequestExecutionResult(WebRequest request)
        {
            _request = request;
        }

        public void Execute(ExecutionContext context)
        {
            try
            {
                _request.BeginGetResponse(ar => OnCompleted(new ResultCompletionEventArgs()), null);
            }
            catch (Exception)
            {
                OnCompleted(new ResultCompletionEventArgs { WasCancelled = true });
            }
        }

        protected virtual void OnCompleted(ResultCompletionEventArgs e)
        {
            var handler = Completed;
            if (handler != null) handler(this, e);
        }
    }
}
