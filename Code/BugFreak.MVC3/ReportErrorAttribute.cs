using System.Web.Mvc;
using BugFreak;

namespace BugFreak.MVC3
{
    public class ReportErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ReportingService.Instance.BeginReport(filterContext.Exception);
        }
    }
}