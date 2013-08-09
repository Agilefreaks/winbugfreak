==================================================
SETUP
==================================================

In you App.xaml.cs

	using BugFreak;

	public partial class App
	{
	    protected override void OnStartup(System.Windows.StartupEventArgs e)
	    {
	        base.OnStartup(e);

	        GlobalConfig.Settings.ApiKey = "[api]";
	        GlobalConfig.Settings.Token = "[token]";

	        BugFreak.Hook();
	    }
	}