using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;
using StudiekollenNew.ViewModels;

namespace StudiekollenNew.Controllers
{
    public class QuestionController : Controller
    {
        public ActionResult DeleteQuestion(int questionId, int testId)
        {
            var questionService = new QuestionService(new RepositoryFactory());

            questionService.RemoveQuestion(questionId);

            return RedirectToAction("HandleTest", "Test", new { id = testId });
        }


        public ViewResult UpdateQuestion(int questionId, string testName, int testId)
        {
         
            var questionService = new QuestionService(new RepositoryFactory());

            var questionModel = questionService.GetQuestion(questionId);
         
            var viewModel = new EditQuestionViewModel
            {     
                TestId = testId,
                Name = testName,
                Query = questionModel.Query,
                Answer = questionModel.Answer,
                QuestionId = questionId
        
            };

            TempData["viewModel"] = viewModel;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(Question questionModel)
        {
            var tempModel = TempData["viewModel"] as EditQuestionViewModel;

            TempData.Keep();

            if (!ModelState.IsValid)
            {
                var viewModel = new EditQuestionViewModel
                {
                    Name = tempModel.Name,
                    Query = tempModel.Query,
                    Answer = tempModel.Answer
                };

                return View(viewModel);
            }

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.UpdateQuestion(questionModel,tempModel.QuestionId);

            return RedirectToAction("HandleTest", "Test", new { id = tempModel.TestId});
        }

        public ActionResult AddQuestionToTest(string testName, int testId)
        {
            var vievModel = new CreateTestViewModel()
            {
                Name = testName,
                TestId = testId

            };

            TempData["viewModel"] = vievModel;


            return View(vievModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestionToTest(Question questionModel)
        {
            var tempModel = TempData["viewModel"] as CreateTestViewModel;

            TempData.Keep();

            if (!ModelState.IsValid)
            {
                var viewModel = new CreateTestViewModel
                {
                    Name = tempModel.Name,
                    Query = questionModel.Query,
                    Answer = questionModel.Answer
                };

                return View(viewModel);
             }

            var testId = tempModel.TestId;

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.AddQuestionsToTest(testId, questionModel);

            return RedirectToAction("HandleTest", "Test", new { id = testId });

        }


    }

}
