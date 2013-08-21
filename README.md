Winbugfreak
===========

Bug tracking client for wpf/silverlight applications, see [http://bugfreak.co](http://bugfreak.co)

Overview
========

A bare bones bug tracking framework that you can use and deploy yourself, it's main aim is to integrate seemesly 
with your application and centralize issues in a main repository, we are working on having a free server deployed
please contact us if you can help

Instalation
===========

You can grab the package from [nuget](http://www.nuget.org/)

For wpf
```
PM> Install-Package BugFreak.WPF
```

For Silverlight
```
PM> Install-Package BugFreak.Silverlight
```

For WinRT
```
PM> Install-Package BugFreak.WinRT
```

Setup
=============

For WPF add a hook in your App.xaml.cs#OnStartup method
```csharp
  using BugFreak;

  public partial class App
  {
      protected override void OnStartup(System.Windows.StartupEventArgs e)
      {
          base.OnStartup(e);

          GlobalConfig.Settings.ApiKey = "ApiKey";
          GlobalConfig.Settings.Token = "Token";

          BugFreak.Hook();
      }
  }

```

For Silverlight add a hook it up in your App.xaml.cs

```csharp
  using BugFreak;

  public partial class App
  {
      public App()
      {
          this.Startup += this.Application_Startup;

          InitializeComponent();
      }

      private void Application_Startup(object sender, StartupEventArgs e)
      {
          this.RootVisual = new MainPage();

          GlobalConfig.Settings.ApiKey = "ApiKey";
          GlobalConfig.Settings.Token = "Token";

          BugFreak.Hook();
      }
  }
```

For WinRT add a hook it up in your App.xaml.cs

```csharp
  using BugFreak;

  public partial class App
  {
	protected override void OnLaunched(LaunchActivatedEventArgs args)
	{
		GlobalConfig.ApiKey = "[apiKey]";
		GlobalConfig.Token = "[token]";
	
		BugFreak.WinRT.BugFreak.Hook();

		Frame rootFrame = Window.Current.Content as Frame;
		// ...
	}
  }
```

That's all folks, any uncatched exceptions will be reported back to the server

Contributing
============

1. Fork it.
2. Create a branch (git checkout -b my_cool_feature)
3. Commit your changes (git commit -am "Added CoolFeature")
4. Push to the branch (git push origin my_cool_feature)
5. Open a Pull Request
