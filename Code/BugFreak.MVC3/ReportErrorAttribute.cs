using System.Web.Mvc;

namespace Bugfreak.MVC3
{
    public class ReportErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Bugfreak.BugFreak.Instance.BeginReport(filterContext.Exception);
        }
    }
}