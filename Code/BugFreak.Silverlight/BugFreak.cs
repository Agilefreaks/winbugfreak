namespace Bugfreak.Silverlight
{
    using System;
    using System.Windows;

    public class BugFreak
    {
        public static void Hook()
        {
            var app = Application.Current;

            Bugfreak.BugFreak.Init();
            app.Exit += OnExit;
            app.UnhandledException += OnException;
        }

        private static void OnException(object sender, ApplicationUnhandledExceptionEventArgs eventArgs)
        {
            Bugfreak.BugFreak.Instance.BeginReport(eventArgs.ExceptionObject);

            eventArgs.Handled = true;
        }

        private static void OnExit(object sender, EventArgs eventArgs)
        {
            var app = Application.Current;

            app.UnhandledException -= OnException;
            app.Exit -= OnExit;
            Bugfreak.BugFreak.Dispose();
        }
    }
}
