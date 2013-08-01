==================================================
CONFIGURATION
==================================================

Add in App.config:

<configSections>
    <section name="BugFreak" type="System.Configuration.AppSettingsSection, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
</configSections>

<BugFreak>
    <add key="ServiceEndpoint" value="http://endpoint.com"/>
    <add key="ApiKey" value="apiKey"/>
    <add key="Token" value="token"/>
</BugFreak>

==================================================
INITIALIZATION
==================================================

Go to App.xaml.cs
Method Main() and write before application.Run()

    BugFreak.Integration.WPF.AgileReporter.Hook();