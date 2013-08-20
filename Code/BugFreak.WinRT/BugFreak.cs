namespace BugFreak.WinRT
{
    using global::BugFreak.WinRT.Components;
    using Windows.UI.Xaml;
    
    public static class BugFreak
    {
        public static void Hook()
        {
            var app = Application.Current;
            
            global::BugFreak.GlobalConfig.ErrorDataProviders.Add(new PackageInfoProvier());
            ReportingService.Init();
            
            app.UnhandledException += OnException;
        }

        private static void OnException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            var exception = eventArgs.Exception;
            exception.Data.Add("Message", eventArgs.Message);

            ReportingService.Instance.BeginReport(exception);
        }
    }
}
