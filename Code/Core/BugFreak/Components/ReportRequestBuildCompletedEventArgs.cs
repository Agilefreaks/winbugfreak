using System;
using System.Net;

namespace BugFreak.Components
{
    public class ReportRequestBuildCompletedEventArgs : EventArgs
    {
        public WebRequest Result { get; set; }
    }
}