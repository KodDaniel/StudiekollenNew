using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudiekollenNew.Controllers
{
    // AllowAnonumous tillåter vem som helt access, i det här fallet till alla Actions i HomeController.
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

    }
}