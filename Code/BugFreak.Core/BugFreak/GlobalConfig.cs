using BugFreak.Components;
using BugFreak.Framework;

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

        public static IServiceLocator ServiceLocator { get; set; }

        public static IErrorReportSerializer ErrorReportSerializer { get; set; }
    }
}
