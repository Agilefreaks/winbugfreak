using BugFreak.Components;
using BugFreak.Infrastructure;
using System.Web.Mvc;

namespace BugFreak
{
    public class BugFreak
    {
        public static void Hook(string apiKey, string token)
        {
            GlobalConfig.ApiKey = apiKey;
            GlobalConfig.Token = token;

            ReportingService.Init();
            GlobalConfig.ErrorDataProviders.Add(new RequestErrorDataProvider());
            GlobalConfig.ErrorDataProviders.Add(new SessionErrorDataProvider());
            GlobalConfig.ErrorDataProviders.Add(new UserErrorDataProvider());
            
            GlobalFilters.Filters.Add(new ReportErrorAttribute());
        }
    }
}
