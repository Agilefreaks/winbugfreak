using System;
using System.Collections.Generic;

namespace BugFreak.Framework
{
    public class SequentialResult : IResult
    {
        private ExecutionContext _context;
        readonly IEnumerator<IResult> _enumerator;

        public SequentialResult(IEnumerator<IResult> enumerator)
        {
            _enumerator = enumerator;
        }

        public SequentialResult(IEnumerable<IResult> enumerable)
        {
            _enumerator = enumerable.GetEnumerator();
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        public void Execute(ExecutionContext context)
        {
            _context = context;
            ChildCompleted(null, new ResultCompletionEventArgs());
        }

        void ChildCompleted(object sender, ResultCompletionEventArgs args)
        {
            var previous = sender as IResult;
            if (previous != null)
            {
                previous.Completed -= ChildCompleted;
            }

            if (args.Error != null || args.WasCancelled)
            {
                OnComplete(args.Error, args.WasCancelled);
                return;
            }

            bool moveNextSucceeded;
            try
            {
                moveNextSucceeded = _enumerator.MoveNext();
            }
            catch (Exception ex)
            {
                OnComplete(ex, false);
                return;
            }

            if (moveNextSucceeded)
            {
                try
                {
                    var next = _enumerator.Current;
                    next.Completed += ChildCompleted;
                    next.Execute(_context);
                }
                catch (Exception ex)
                {
                    OnComplete(ex, false);
                }
            }
            else
            {
                OnComplete(null, false);
            }
        }

        void OnComplete(Exception error, bool wasCancelled)
        {
            _enumerator.Dispose();
            Completed(this, new ResultCompletionEventArgs { Error = error, WasCancelled = wasCancelled });
        }
    }
}