using System;
using System.Web.Mvc;

namespace MVC3.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Boom()
        {
            throw new Exception("Boom!");
        }
    }
}
