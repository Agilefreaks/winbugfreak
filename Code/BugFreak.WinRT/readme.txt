==================================================
SETUP
==================================================

Check sample app at https://github.com/Agilefreaks/winbugfreak/tree/master/Code/Samples/WinRT.Sample
In you App.xaml.cs

	using BugFreak;

	public partial class App : Application
	{
	    protected override void OnLaunched(LaunchActivatedEventArgs args)
	    {
            	GlobalConfig.ApiKey = "[apiKey]";
            	GlobalConfig.Token = "[token]";
            
            	BugFreak.WinRT.BugFreak.Hook();

            	Frame rootFrame = Window.Current.Content as Frame;
		// ...
	    }

	    // ...
	}

In Package.appxmanifest in Capabilities tab make sure you check Internet