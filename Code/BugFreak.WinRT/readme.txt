==================================================
SETUP
==================================================

Check sample app at https://github.com/Agilefreaks/winbugfreak/tree/master/Code/Samples/WinRT.Sample
In you App.xaml.cs

namespace MyNamespace
{
    using BugFreak;

    public partial class App : Application
    {
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            BugFreak.Hook("ApiKey", "Token", this);

            Frame rootFrame = Window.Current.Content as Frame;
	}
    }
}

In Package.appxmanifest in Capabilities tab make sure you check Internet