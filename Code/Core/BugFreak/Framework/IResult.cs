using System;

namespace Bugfreak.Framework
{
    public interface IResult
    {
        event EventHandler<ResultCompletionEventArgs> Completed;

        void Execute(ExecutionContext context);
    }
}
