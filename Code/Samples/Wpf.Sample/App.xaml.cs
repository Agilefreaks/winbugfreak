namespace Wpf.Sample
{
    using BugFreak;

    public partial class App
    {
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            base.OnStartup(e);

            // Demo credentials:
            // bugfreak.co
            // u: demo@bugfreak.co
            // p: demouser
            GlobalConfig.Settings.ApiKey = "9dd7f8aa-6b29-4022-80fa-441609ca1547";
            GlobalConfig.Settings.Token = "5204d8a0d2443315be000003";

            BugFreak.Hook();
        }
    }
}
