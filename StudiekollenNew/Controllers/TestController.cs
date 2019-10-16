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

    [Authorize]
    public class TestController : Controller
    {
        [Authorize]
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

            return RedirectToAction("HandleTest", "Test", new { testId = tempModel.TestId });
        }


        public ActionResult SearchForTest()
        {
            var testService = new TestService(new RepositoryFactory());

            var userId = User.Identity.GetUserId();

            var allTestsForThisUser = testService.GetAllTestsForThisUserId(userId);

            if (!allTestsForThisUser.Any())
            {
                return View("_partialDetails");
            }

            var vievModel = new SearchTestViewModel
            {
                AllTests = allTestsForThisUser
            };

            return View(vievModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchForTest(string id)
        {
            return RedirectToAction("HandleTest", new { testId = id });

        }


        public ViewResult DetailsTest()
        {
            var testService = new TestService(new RepositoryFactory());

            var allTests = testService.GetAllTestsForThisUserId(User.Identity.GetUserId());
       
            var viewmodel = new SearchTestViewModel
            {
                AllTests = allTests
            };

            return View(viewmodel);
        }

        public ActionResult DeleteTest(int testId)
        {
            var repoFactory = new RepositoryFactory();

            var testService = new TestService(repoFactory);

            testService.DeleteTest(testId);

            return RedirectToAction("DetailsTest");
        }

        public ViewResult HandleTest(int testId)
        {
            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            var testService = new TestService(repoFactory);

            var testName = testService.GetTest(testId).Name;

            var questionModels = questionService.GetAllQuestions(testId);

            var viewModel = new HandleTestViewModel
            {
                TestId = testId,
                TestName = testName,
                QuestionsModels = questionModels
            };

            return View(viewModel);
        }

        
    }
}



