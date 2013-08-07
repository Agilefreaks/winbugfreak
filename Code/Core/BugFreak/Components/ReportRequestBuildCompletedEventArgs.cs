using System;
using System.Net;

namespace Bugfreak.Components
{
    public class ReportRequestBuildCompletedEventArgs : EventArgs
    {
        public WebRequest Result { get; set; }
    }
}