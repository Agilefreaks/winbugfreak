using System;
using BugFreak.Components;

namespace BugFreak
{
    public class GlobalConfig
    {
        public class Settings
        {
            public static string ServiceEndPoint { get; set; }

            public static string ApiKey { get; set; }

            public static string AppName { get; set; }

            public static string Token { get; set; }
        }

        public static IServiceProvider ServiceProvider { get; set; }

        public static IErrorReportSerializer ErrorReportSerializer { get; set; }
    }
}
