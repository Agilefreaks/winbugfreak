﻿using BugFreak.Core.Components;
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
            GlobalConfig.ErrorDataProviders.Add(new RequestErrorDataProvider());
            GlobalConfig.ErrorDataProviders.Add(new SessionErrorDataProvider());
            GlobalConfig.ErrorDataProviders.Add(new UserErrorDataProvider());
            
            GlobalFilters.Filters.Add(new ReportErrorAttribute());
        }
    }
}
