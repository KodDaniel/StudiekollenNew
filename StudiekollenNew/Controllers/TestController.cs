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
        public ActionResult NewTest(NewTestViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                var userId = User.Identity.GetUserId();

                var testService = new TestService(new RepositoryFactory());

                testService.AddTest(viewModel,userId);

                return RedirectToAction("CreateTest", new {testName = viewModel.Name});
            }
       }

        public ActionResult CreateTest(string testName)
        {

            var viewModel = new CreateTestViewModel
            {
                Name = testName
            };
      
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult CreateTest(CreateTestViewModel viewModel)
        {

            var repoFactory = new RepositoryFactory();

            var testService = new TestService(repoFactory);

            var userId = User.Identity.GetUserId();

            var recentTestName = testService.GetMostRecentTestName(userId);

            if (!ModelState.IsValid)
            {
                viewModel.Name = recentTestName;

                return View(viewModel);
            }

            var questionService = new QuestionService(repoFactory);
         
            viewModel.Id = testService.GetMostRecentTestId(userId);

            questionService.AddQuestionsToTest(viewModel);

            return RedirectToAction("CreateTest", new {testName = recentTestName});
        }

        public ViewResult SearchForTest()
        {

            var userService = new UserService(new RepositoryFactory());

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
            if (string.IsNullOrWhiteSpace(userName))
            {
                return RedirectToAction("SearchForTest");
            }
            else
            {                 
                return RedirectToAction("Details", new { userNameSelected = userName});
            }

        }

        public ViewResult Details(string userNameSelected)
        {
            var testService = new TestService(new RepositoryFactory());

            var allTests = testService.GetAllTestsForThisUserName(userNameSelected);

            var viewmodel = new FindTestViewModel
            {
                AllTests = allTests,
                Username = userNameSelected,                     
            };
            
            return View(viewmodel);
        }

        public ActionResult DeleteTest(int id)
        {
            var repoFactory = new RepositoryFactory();

            var testService = new TestService(repoFactory);

            var testModel = testService.GetSingleTestModelByTestId(id);

            var userService = new UserService(repoFactory);

            var userId = User.Identity.GetUserId();

            var userName = userService.GetUserByUserId(userId).UserName;

            testService.RemoveTest(testModel);

            return RedirectToAction("Details", new {userNameSelected = userName});
        }

        public ViewResult EditTest(int id)
        {
            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            var testService = new TestService(repoFactory);

            var testName = testService.GetSingleTestModelByTestId(id).Name;

            var questionModels = questionService.AllQuestionsModelsByTestId(id);

            var viewModel = new EditTestViewModel
            {
                TestId = id,
                TestName = testName,
                QuestionsModels = questionModels
            };

            return View(viewModel);
        }

        
    }
}



