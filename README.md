winbugfreak
===========

A bug tracking framework for all the other frameworks

Dependencies
============

	BugFreak.dll
	BugFreak.Integration.WPF.dll

Usage
=====

<b>WPF</b>

	Config:
	
	Add in <i>App.config</i>:

	<pre>
		<configSections>
			<section name="BugFreak" type="System.Configuration.AppSettingsSection, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
		</configSections>

		<BugFreak>
			<add key="ServiceEndpoint" value="http://endpoint.ro"/>
			<add key="ApiKey" value="apiKey"/>
			<add key="AppName" value="appName"/>
			<add key="Token" value="token"/>
		</BugFreak>
	<pre>

	Initialize:

		BugFreak.Integration.WPF.AgileReporter.Hook();
			

<b>SilverLight 4</b>
	
	Config:
		Add in App.xaml.cs in Application_Startup:
		
		BugFreak.GlobalConfig.Settings.ApiKey = "apiKey";
		BugFreak.GlobalConfig.Settings.Token = "token";
		BugFreak.GlobalConfig.Settings.AppName = "AppName";
		BugFreak.GlobalConfig.Settings.ServiceEndpoint = "http://service.com";
		
		BugFreak.Integration.Silverlight.AgileReporter.Hook();
		
			
Sample Request
==============

```text
POST http://domain.com/ HTTP/1.1
Api-Key: apiKey
Token: token
App-Name: MyTest.vshost.exe
Host: domain.com
Content-Type: application/x-www-form-urlencoded
Content-Length: 1579
Expect: 100-continue
Connection: Keep-Alive

message='The%20invocation%20of%20the%20constructor%20on%20type%20'MyTest.MainWindow'%20that%20matches%20the%20specified%20binding%20constraints%20threw%20an%20exception.'%20Line%20number%20'3'%20and%20line%20position%20'9'.&source=PresentationFramework&stackTrace=%20%20%20at%20System.Windows.Markup.WpfXamlLoader.Load(XamlReader%20xamlReader%2C%20IXamlObjectWriterFactory%20writerFactory%2C%20Boolean%20skipJournaledProperties%2C%20Object%20rootObject%2C%20XamlObjectWriterSettings%20settings%2C%20Uri%20baseUri)%0D%0A%20%20%20at%20System.Windows.Markup.WpfXamlLoader.LoadBaml(XamlReader%20xamlReader%2C%20Boolean%20skipJournaledProperties%2C%20Object%20rootObject%2C%20XamlAccessLevel%20accessLevel%2C%20Uri%20baseUri)%0D%0A%20%20%20at%20System.Windows.Markup.XamlReader.LoadBaml(Stream%20stream%2C%20ParserContext%20parserContext%2C%20Object%20parent%2C%20Boolean%20closeStream)%0D%0A%20%20%20at%20System.Windows.Application.LoadBamlStreamWithSyncInfo(Stream%20stream%2C%20ParserContext%20pc)%0D%0A%20%20%20at%20System.Windows.Application.LoadComponent(Uri%20resourceLocator%2C%20Boolean%20bSkipJournaledProperties)%0D%0A%20%20%20at%20System.Windows.Application.DoStartup()%0D%0A%20%20%20at%20System.Windows.Application.%3C.ctor%3Eb__1(Object%20unused)%0D%0A%20%20%20at%20System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate%20callback%2C%20Object%20args%2C%20Int32%20numArgs)%0D%0A%20%20%20at%20MS.Internal.Threading.ExceptionFilterHelper.TryCatchWhen(Object%20source%2C%20Delegate%20method%2C%20Object%20args%2C%20Int32%20numArgs%2C%20Delegate%20catchHandler)
```

The request needs to have:
- in header:`ApiKey`, `Token`,`AppName` 
- in body: `message`, `source` and `stackTrace`

From curl:
```
curl -d message=message -d source=source -d stackTrace=stackTrace 'http://localhost:3000/v1/api/errors' -H Content-Type:application/x-www-form-urlencoded -H "Token: 123token" -H "Api-Key: 123ApiKey" -H "App-Name: MyTest.vshost.exe" -v
```
