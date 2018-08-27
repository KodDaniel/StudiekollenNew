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
        // Vi vill inte ska skapa en instans av databasklassen i varje metod, lös detta!
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
            var testModel = TempData["testModel"] as Test;

            if (testModel is null)
            {
                viewModel.Name = "Placeholder";
            }
            else
            {
                viewModel.Name = testModel.Name; 
            }

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
            return RedirectToAction("CreateTest"); 
            
        }

    }
}




//return RedirectToAction("CreateTest", new{setTestName = questionModel.Test.Name }); 



////if(nameOfTest is null)
////{
////    viewModel.Name = "Hårdkodat provnamn";
////}
////else
////{
////    viewModel.Name = nameOfTest.Name;
////}






















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

