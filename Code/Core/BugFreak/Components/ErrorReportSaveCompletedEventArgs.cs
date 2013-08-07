using System;

namespace Bugfreak.Components
{
    public class ErrorReportSaveCompletedEventArgs : EventArgs
    {
        public bool Success { get; set; }
    }
}