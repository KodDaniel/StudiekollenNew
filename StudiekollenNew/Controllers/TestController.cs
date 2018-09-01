using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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

      
        public ViewResult NewTest()
        {
            var viewModel = new NewTestViewModel();
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult NewTest(Test testModel)
        {
            var viewModel = new NewTestViewModel();         
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
                testService.AddTest(testModel);
                // TempData is useful when you want to transfer non-sensitive data 
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

            var repoFactory = new RepositoryFactory();
            var testService = new TestService(repoFactory);
            var recentTestId = testService.GetMostRecentTestId(currentUserId);
            var recentTestName = testService.GetMostRecentTestName(currentUserId);

            var questionService = new QuestionService(repoFactory);
            questionService.AddQuestionsToTest(recentTestId,questionModel);
            return RedirectToAction("CreateTest", new {testName = recentTestName});
        }

        public ViewResult SearchForTest()
        {
       
            var repoFactory = new RepositoryFactory();
            var userService = new UserService(repoFactory);
            var allUsers = userService.GetAllUsers();
            var vievModel = new FindTestViewModel
            {
                Users = allUsers,             
            };
         

            return View(vievModel);
        }

        [HttpPost]
        public ActionResult SearchForTest(string userName)
        {
            // Inget fel på if-satsen, men: sidan laddar om till skillnad från om du använder "Modelstate.Isvalid". Kan vara värt att byta till det med andra ord.
            if (string.IsNullOrWhiteSpace(userName))
            {

                return RedirectToAction("SearchForTest");
            }
            else
            {
                var repoFactory = new RepositoryFactory();
                var testService = new TestService(repoFactory);
                // TempData is useful when you want to transfer non-sensitive data 
                TempData["result"] = testService.GetTestsForThisUserName(userName);
                return RedirectToAction("Details", new {currentUsername = userName});
            }
           
        }

        public ViewResult Details(string currentUsername)
        {
            var result = TempData["result"] as IEnumerable<Test>;
            var viewmodel = new FindTestViewModel
            {
                AllTests = result,
                Username = currentUsername
            };

            return View(viewmodel);
        }


        public ViewResult HandleTest (int id)
        {           
            var repoFactory = new RepositoryFactory();
            var testService = new TestService(repoFactory);
            var testModel = testService.GetSingleTestByTestId(id);
            var deleteTestModel = new DeleteTestViewModel()
            {               
                Name = testModel.Name
            };

            TempData["testModel"] = testModel;

            return View(deleteTestModel);
        }

   
        public ActionResult DeleteTest()
        {
            var repoFactory = new RepositoryFactory();
            var testService = new TestService(repoFactory);
            var testModel = TempData["testModel"] as Test;
            testService.RemoveTest(testModel);
            return RedirectToAction("Index","Home");
        }

        public ActionResult EditTest()
        {
            return View();
        }

    }
}


