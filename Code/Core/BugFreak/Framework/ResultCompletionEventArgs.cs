using System;

namespace Bugfreak.Framework
{
    public class ResultCompletionEventArgs : EventArgs
    {
        public Exception Error { get; set; }

        public bool WasCancelled { get; set; }
    }
}