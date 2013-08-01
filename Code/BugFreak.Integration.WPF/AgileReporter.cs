using System.Collections.Specialized;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace BugFreak.Integration.WPF
{
    public class AgileReporter
    {
        public static void Hook()
        {
            var app = Application.Current;

            ReadSettings();
            
            BugFreak.AgileReporter.Init();
            app.Exit += OnExit;
            app.DispatcherUnhandledException += OnException;
        }

        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            BugFreak.AgileReporter.Instance.BeginReport(eventArgs.Exception);
        }

        private static void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            var app = Application.Current;

            app.DispatcherUnhandledException -= OnException;
            app.Exit -= OnExit;
            BugFreak.AgileReporter.Dispose();
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
