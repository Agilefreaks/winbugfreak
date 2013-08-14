==================================================
CONFIGURATION
==================================================

Add in Web.config:

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

Go to Global.asax.cs
Method Application_Start and put at the beginning

    Bugfreak.MVC3.BugFreak.Hook();