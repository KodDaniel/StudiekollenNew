using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudiekollenNew.DataBase;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;

namespace StudiekollenNew.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            return View();


        }

        public ViewResult ToDo()
        {
            return View();
        }

    }
}