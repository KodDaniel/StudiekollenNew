using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudiekollenNew.DataBase;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;
using StudiekollenNew.ViewModels;

namespace StudiekollenNew.Controllers
{
  
    public class TestController : Controller
    {

      
        public ActionResult NewTest()
        {
            var viewModel = new NewTestViewModel();
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult NewTest(Test testModel)
        {
            var viewModel = new NewTestViewModel();
            var db = ContextSingelton.GetContext();           
            testModel.UserId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {             
                return View(viewModel);
            }
            else
            {
                testModel.UserId = User.Identity.GetUserId();
                var repoFactory = new RepositoryFactory();
                var testService = new TestService(repoFactory);
                testService.Add(testModel);
                db.SaveChanges();

                TempData["testModel"] = testModel;

                return RedirectToAction("CreateTest");
            }
          
        }

        public ActionResult CreateTest(string testName)
        {

            var viewModel = new CreateTestViewModel();
            var testModel = TempData["testModel"] as Test;

            if (testModel is null)
            {
                viewModel.Name = testName;
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
            var currentUserId = User.Identity.GetUserId();

            var db = ContextSingelton.GetContext();

            var repoFactory = new RepositoryFactory();
            var testService = new TestService(repoFactory);
            var recentTestId = testService.GetMostRecentTestId(currentUserId);
            var recentTestName = testService.GetMostRecentTestName(currentUserId);

            questionModel.TestId = recentTestId;

            db.Question.Add(questionModel);
            
            db.SaveChanges();            

            return RedirectToAction("CreateTest", new {testName = recentTestName});
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

