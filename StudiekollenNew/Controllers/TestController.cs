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
        [ValidateAntiForgeryToken]
        public ActionResult NewTest(Test testModel)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new NewTestViewModel
                {
                    Name = testModel.Name
                };

                return View(viewModel);
            }

            var userId = User.Identity.GetUserId();

            var testService = new TestService(new RepositoryFactory());

            testService.AddTest(testModel, userId);

            return RedirectToAction("CreateTest", new {testName = testModel.Name});
        
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
        [ValidateAntiForgeryToken]
        public ActionResult CreateTest(Question questionModel)
        {
            var repoFactory = new RepositoryFactory();

            var testService = new TestService(repoFactory);
            
            var userId = User.Identity.GetUserId();

            var recentTestName = testService.GetMostRecentTest(userId).Name;

            if (!ModelState.IsValid)
            {
                var viewModel = new CreateTestViewModel
                {
                    Name = recentTestName,
                    Query = questionModel.Query,
                    Answer = questionModel.Answer
                };

                return View(viewModel);
            }

            var testId = testService.GetMostRecentTest(userId).Id;

            var questionService = new QuestionService(repoFactory);

            questionService.AddQuestionsToTest(testId,questionModel);

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
        [ValidateAntiForgeryToken]
        public ActionResult SearchForTest(string userName)
        {
            // If-satsen kan ta bort helt om du listar ut hur du kan göra alternativet "välj användare" unselectable via koden i din view.
            if (string.IsNullOrWhiteSpace(userName))
            {
                return RedirectToAction("SearchForTest");
            }

            return RedirectToAction("Details", new { userNameSelected = userName});
            
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

            var testModel = testService.GetTest(id);

            var userService = new UserService(repoFactory);

            var userId = User.Identity.GetUserId();

            var userName = userService.GetUser(userId).UserName;

            testService.RemoveTest(testModel);

            return RedirectToAction("Details", new {userNameSelected = userName});
        }

        public ViewResult EditTest(int id)
        {
            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            var testService = new TestService(repoFactory);

            var testName = testService.GetTest(id).Name;

            var questionModels = questionService.GetAllQuestions(id);

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



