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
using StudiekollenNew.ViewModels.TestViewModels;

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
            questionService.AddQuestionsToTest(recentTestId, questionModel);

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
                TempData["result"] = testService.GetAllTestsForThisUserName(userName);

                return RedirectToAction("Details", new {currentUsername = userName});
            }

        }

        public ViewResult Details(string currentUsername)
        {
            var result = TempData["result"] as IEnumerable<Test>;           
            var viewmodel = new FindTestViewModel
            {
                AllTests = result,
                Username = currentUsername,
                
            };

            return View(viewmodel);
        }

        public ActionResult DeleteTest(int id)
        {
            var repoFactory = new RepositoryFactory();
            var testService = new TestService(repoFactory);
            var testModel = testService.GetSingleTestByTestId(id);
            var userService = new UserService(repoFactory);
            var userName = userService.GetUserByUserId(User.Identity.GetUserId()).UserName;
            testService.RemoveTest(testModel);
            TempData["result"] = testService.GetAllTestsForThisUserName(userName);

            return RedirectToAction("Details", new {currentUsername = userName});
        }

        public ViewResult EditTest(int id)
        {
            var repoFactory = new RepositoryFactory();
            var questionService = new QuestionService(repoFactory);
            var testService = new TestService(repoFactory);
            var testName = testService.GetSingleTestByTestId(id).Name;
            var questionModels = questionService.AllQuestionsModelsByTestId(id);

            var viewModel = new EditTestViewModel
            {
                TestName = testName,
                QuestionsModels = questionModels
            };

            return View(viewModel);
        }

        
    }
}



