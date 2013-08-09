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

            GlobalConfig.Settings.ApiKey = "9dd7f8aa-6b29-4022-80fa-441609ca1547";
            GlobalConfig.Settings.Token = "5204f224d2443315be000027";

            BugFreak.Hook();
        }
