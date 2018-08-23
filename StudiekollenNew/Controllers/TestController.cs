using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudiekollenNew.Controllers
{
    public class TestController : Controller
    {
        public ActionResult CreateTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTest(string tillfälligVaribelSåKompilatornInteblirSur)
        {
            throw new NotImplementedException();
        }
    }
}