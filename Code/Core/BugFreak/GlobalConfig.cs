namespace BugFreak
{
    using System.Collections.Generic;

    using global::BugFreak.Components;
    using global::BugFreak.Framework;

    public class GlobalConfig
    {
        public class Settings
        {
            public static string ServiceEndPoint { get; set; }

            public static string ApiKey { get; set; }

            public static string Token { get; set; }
        }

        public static IServiceLocator ServiceLocator { get; set; }

        public static IErrorReportSerializer ErrorReportSerializer { get; set; }

        public static List<IErrorDataProvider> ErrorDataProviders { get; set; }

        static GlobalConfig()
        {
            ErrorDataProviders = new List<IErrorDataProvider>();
        }
    }
}
