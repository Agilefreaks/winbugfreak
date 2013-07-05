using System;
using System.Windows;
using System.Windows.Threading;

namespace AgileBug.WPF
{
    public class AgileReporter
    {
        public static void Hook()
        {
            var app = Application.Current;

            GlobalConfig.Settings.AppName = AppDomain.CurrentDomain.FriendlyName;
            AgileBug.AgileReporter.Init();
            app.Exit += OnExit;
            app.DispatcherUnhandledException += OnException;
        }

        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            AgileBug.AgileReporter.Instance.BeginReport(eventArgs.Exception);
            
            eventArgs.Handled = true;
        }

        private static void OnExit(object sender, ExitEventArgs exitEventArgs)
        {
            var app = Application.Current;

            app.DispatcherUnhandledException -= OnException;
            app.Exit -= OnExit;
            AgileBug.AgileReporter.Dispose();
        }
    }
}
