==================================================
SETUP
==================================================

Check sample app at https://github.com/Agilefreaks/winbugfreak/tree/master/Code/Silverlight.Sample
In your App.xaml.cs

    using BugFreak;

    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();

            GlobalConfig.Settings.ApiKey = "[apiKey]";
            GlobalConfig.Settings.Token = "[token]";

            BugFreak.Hook();
        }
