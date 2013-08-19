using BugFreak;

namespace Bugfreak.MVC3
{
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Web.Mvc;

    public class BugFreak
    {
        public static void Hook()
        {
            ReadSettings();

            ReportingService.Init();

            GlobalFilters.Filters.Add(new ReportErrorAttribute());
        }

        private static void ReadSettings()
        {
            var configSection = ConfigurationManager.GetSection("BugFreak") as NameValueCollection;
            if (configSection != null)
            {
                GlobalConfig.Settings.ServiceEndPoint = configSection["ServiceEndpoint"];
                GlobalConfig.Settings.ApiKey = configSection["ApiKey"];
                GlobalConfig.Settings.Token = configSection["Token"];
            }
        }
    }
}
