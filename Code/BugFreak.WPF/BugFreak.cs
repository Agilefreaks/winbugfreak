namespace BugFreak
{
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Windows;
    using System.Windows.Threading;

    using global::BugFreak.Components;

    public class BugFreak
    {
        /// <summary>
        /// User Hook don't instantiate this class
        /// </summary>
        private BugFreak()
        {
        }

        public static void Hook()
        {
            var app = Application.Current;

            ReadSettings();

            ReportingService.Init();
            GlobalConfig.ErrorDataProviders.Add(new WpfErrorDataProvider());

            app.Exit += OnExit;
            app.DispatcherUnhandledException += OnException;
        }

        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            ReportingService.Instance.BeginReport(eventArgs.Exception);
        }

        private static void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            var app = Application.Current;

            app.DispatcherUnhandledException -= OnException;
            app.Exit -= OnExit;
            ReportingService.Dispose();
        }

        private static void ReadSettings()
        {
            var configSection = ConfigurationManager.GetSection("BugFreak") as NameValueCollection;
            if (configSection != null)
            {
                GlobalConfig.ServiceEndPoint = configSection["ServiceEndpoint"];
                GlobalConfig.ApiKey = configSection["ApiKey"];
                GlobalConfig.Token = configSection["Token"];
            }
        }
    }
}
