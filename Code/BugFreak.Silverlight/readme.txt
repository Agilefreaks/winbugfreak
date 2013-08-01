==================================================
CONFIGURATION
==================================================

In App.xaml.cs#Application_Startup

BugFreak.GlobalConfig.Settings.ApiKey = "apiKey";
BugFreak.GlobalConfig.Settings.Token = "token";
BugFreak.GlobalConfig.Settings.ServiceEndpoint = "http://service.com";

BugFreak.Integration.Silverlight.AgileReporter.Hook();