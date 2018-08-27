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

        [HttpPost]
        public ActionResult NewTest(Test testModel)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new NewTestViewModel();

                return View(viewModel);
            }
            else
            {
                TempData["testModel"] = testModel;

                return RedirectToAction("CreateTest");
            }
          
        }

        public ActionResult CreateTest()
        {
            var viewModel = new CreateTestViewModel();
            var nameOfTest = TempData["testModel"] as Test;
            viewModel.Name = nameOfTest.Name;

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult CreateTest(Question questionModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CreateTestViewModel();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("CreateTest"); 
            }

        }

    }
}







//if(nameOfTest is null)
//{
//    viewModel.Name = "Hårdkodat provnamn";
//}
//else
//{
//    viewModel.Name = nameOfTest.Name;
//}






















//[HttpPost]
//public ActionResult CreateTest(Question questionModel)
//{
//    var currentUserId = User.Identity.GetUserId();

//    var _context = new ApplicationDbContext();

//    // Substitut för Last-operator.
//    var getTestId = _context.Test.OrderByDescending(c=>c.Id).First(c => c.UserId == currentUserId);

//    questionModel.TestId = getTestId.Id;

//    _context.Question.Add(questionModel);

//    _context.SaveChanges();

//    return RedirectToAction("CreateTest", "Test");
//}

