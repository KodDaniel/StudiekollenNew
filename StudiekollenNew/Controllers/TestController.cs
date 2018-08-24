using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudiekollenNew.Models;

namespace StudiekollenNew.Controllers
{
    public class TestController : Controller
    {
        //private readonly ApplicationDbContext _context;

        public ActionResult CreateTest()
        {

            return View();
        }

        // Ändra så jag inte använder mig av Domain-klassen utan av ViewModel-klassen
        [HttpPost]
        public ActionResult CreateTest(Test testModel)
        {
            testModel.UserId = User.Identity.GetUserId();

            var _context = new ApplicationDbContext();

            _context.Test.Add(testModel);

            _context.SaveChanges();

            return RedirectToAction("CreateTest", "Test");           
        }    
    }
}