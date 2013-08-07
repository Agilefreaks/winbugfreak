namespace Bugfreak.WPF
{
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Windows;
    using System.Windows.Threading;

    public class BugFreak
    {
        public static void Hook()
        {
            var app = Application.Current;

            ReadSettings();

            Bugfreak.BugFreak.Init();
            app.Exit += OnExit;
            app.DispatcherUnhandledException += OnException;
        }

        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            Bugfreak.BugFreak.Instance.BeginReport(eventArgs.Exception);
        }

        private static void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            var app = Application.Current;

            app.DispatcherUnhandledException -= OnException;
            app.Exit -= OnExit;
            Bugfreak.BugFreak.Dispose();
        }

        private static void ReadSettings()
        {
            var configSection = ConfigurationManager.GetSection("BugFreak") as NameValueCollection;
            if (configSection != null)
            {
                GlobalConfig.Settings.ServiceEndPoint = configSection["ServiceEndpoint"];
                GlobalConfig.Settings.ApiKey = configSection["ApiKey"];
                GlobalConfig.Settings.Token = configSection["Token"];
            }
        }
    }
}
