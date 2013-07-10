using System;

namespace BugFreak.Components
{
    public class ErrorReportSaveCompletedEventArgs : EventArgs
    {
        public bool Success { get; set; }
    }
}