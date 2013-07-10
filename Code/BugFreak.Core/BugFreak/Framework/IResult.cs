using System;

namespace BugFreak.Framework
{
    public interface IResult
    {
        event EventHandler<ResultCompletionEventArgs> Completed;

        void Execute(ExecutionContext context);
    }
}
