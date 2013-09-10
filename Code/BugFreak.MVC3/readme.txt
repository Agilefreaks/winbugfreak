==================================================
INITIALIZATION
==================================================

Go to Global.asax.cs

using BugFreak;

public class MvcApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
        BugFreak.Hook("ApiKey", "Token");
    }
}