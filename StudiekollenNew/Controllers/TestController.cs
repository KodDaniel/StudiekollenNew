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

            testService.AddTest(testModel,userId);

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

            questionService.AddQuestion(testId,questionModel);

            return RedirectToAction("CreateTest", new {testName = recentTestName});
        }

        public ViewResult UpdateTest(int testId)
        {
            var testService = new TestService(new RepositoryFactory());

            var test = testService.GetTest(testId);

            testService.UpdateTest(test,testId);

            var viewModel = new UpdateTestViewModel
            {
                Name = test.Name,
                TestId = testId
                
            };

            TempData["viewModel"] = viewModel;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTest(Test test)
        {
            var tempModel = TempData["viewModel"] as UpdateTestViewModel;

            TempData.Keep();

            if (!ModelState.IsValid)
            {
                var viewModel = new UpdateTestViewModel
                {
                    Name = tempModel.Name,
                };

                return View(viewModel);
            }

            var testService = new TestService(new RepositoryFactory());

            testService.UpdateTest(test, tempModel.TestId);

            return RedirectToAction("HandleTest", "Test", new { id = tempModel.TestId });
        }


        public ViewResult SearchForTest()
        {
            var userService = new UserService(new RepositoryFactory());

            var allUsers = userService.GetAllUsers();

            var vievModel = new SearchTestViewModel
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

            return RedirectToAction("DetailsTest", new { userNameSelected = userName});
            
        }

        public ViewResult DetailsTest(string userNameSelected)
        {
            var testService = new TestService(new RepositoryFactory());

            var allTests = testService.GetAllTestsForThisUserName(userNameSelected);

            var viewmodel = new SearchTestViewModel
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

            var userService = new UserService(repoFactory);

            var userId = User.Identity.GetUserId();

            var userName = userService.GetUser(userId).UserName;

            testService.DeleteTest(id);

            return RedirectToAction("DetailsTest", new {userNameSelected = userName});
        }

        public ViewResult HandleTest(int id)
        {
            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            var testService = new TestService(repoFactory);

            var testName = testService.GetTest(id).Name;

            var questionModels = questionService.GetAllQuestions(id);

            var viewModel = new HandleTestViewModel
            {
                TestId = id,
                TestName = testName,
                QuestionsModels = questionModels
            };

            return View(viewModel);
        }

        
    }
}



