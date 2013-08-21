using BugFreak.MVC3;

namespace BugFreak
{
    using System.Web.Mvc;

    public class BugFreak
    {
        public static void Hook(string apiKey, string token)
        {
            GlobalConfig.ApiKey = apiKey;
            GlobalConfig.Token = token;

            ReportingService.Init();
            
            GlobalFilters.Filters.Add(new ReportErrorAttribute());
        }
    }
}
