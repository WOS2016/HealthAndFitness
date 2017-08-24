using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AngularJsNetFrameworkRepos.Controllers
{
    // To give up Authorize, and implement dashboard on index.cshtml.
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexAngularjs()
        {
            return View();
        }

    }

}
