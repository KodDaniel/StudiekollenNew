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
        public ActionResult DeleteQuestion(int questionId,int testId)
        {
            var repoFactory = new RepositoryFactory();
            var questionService = new QuestionService(repoFactory);
            questionService.RemoveQuestionFromTest(questionId);

            return RedirectToAction("EditTest", "Test", new {id = testId});
        }

      
        public ViewResult EditQuestion(int questionId, string testName,int testId)
        {
            var repoFactory = new RepositoryFactory();
            var questionService = new QuestionService(repoFactory);
            var questionModel = questionService.GetSingleQuestionModelByQuestionId(questionId);
            var viewModel = new EditQuestionViewModel
            {
                Id = questionId,
                Name = testName,
                Query = questionModel.Query,
                Answer = questionModel.Answer,
                TestId = testId
            };
            TempData["viewModel"] = viewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditQuestion(Question updatedQuestionModel)
        {
            var viewModel = TempData["viewModel"] as EditQuestionViewModel;
            var questionId = viewModel.Id;
            var repoFactory = new RepositoryFactory();
            var questionService = new QuestionService(repoFactory);
            questionService.UpdateQuestion(updatedQuestionModel,questionId);

            return RedirectToAction("EditTest", "Test", new {id = viewModel.TestId});
        }

      


    }

}
