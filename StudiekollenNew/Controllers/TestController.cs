using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudiekollenNew.Models;
using StudiekollenNew.ViewModels;

namespace StudiekollenNew.Controllers
{
    public class TestController : Controller
    {
        //private readonly ApplicationDbContext _context;

        public ActionResult NewTest()
        {
            var viewModel = new NewTestViewModel();

            return View(viewModel);

        }

        //Ändra så jag inte använder mig av Domain-klassen utan av ViewModel-klassen.Plus refatorisera koden, bland annat Db-connectoin.
        [HttpPost]
        public ActionResult NewTest(Test testModel)
        {
            testModel.UserId = User.Identity.GetUserId();
           

            var _context = new ApplicationDbContext();

            _context.Test.Add(testModel);

            _context.SaveChanges();

            return RedirectToAction("CreateTest", "Test");
        }

        public ActionResult CreateTest()
        {
            var viewModel = new CreateTestViewModel();

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult CreateTest(Question questionModel)
        {
            var currentUserId = User.Identity.GetUserId();

            var _context = new ApplicationDbContext();

            // Substitut för Last-operator.
            var getTestId = _context.Test.OrderByDescending(c=>c.Id).First(c => c.UserId == currentUserId);

            questionModel.TestId = getTestId.Id;

            _context.Question.Add(questionModel);

            _context.SaveChanges();

            return RedirectToAction("CreateTest", "Test");
        }
    }
}