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

            questionService.RemoveQuestionFromTest(questionId);

            return RedirectToAction("EditTest", "Test", new { id = testId });
        }

        public ViewResult EditQuestion(int questionId, string testName, int testId)
        {
            var questionService = new QuestionService(new RepositoryFactory());

            var questionModel = questionService.GetSingleQuestionModelByQuestionId(questionId);

            TempData["idList"] = new List<int> {questionId, testId};

            var viewModel = new EditQuestionViewModel
            {     
                Name = testName,
                Query = questionModel.Query,
                Answer = questionModel.Answer,
        
            };

           

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditQuestion(EditQuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var idList = TempData["idList"] as List<int>;        

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.UpdateQuestion(viewModel, idList[0]);

            return RedirectToAction("EditTest", "Test", new { id = idList[1]});
        }

        public ActionResult AddQuestionToTest(string testName, int testId)
        {
            var vievModel = new CreateTestViewModel()
            {
                Name = testName,
                Id = testId
            };

            return View(vievModel);
        }

        [HttpPost]
        public ActionResult AddQuestionToTest(CreateTestViewModel viewModel)
        {

            var testId = viewModel.Id;

            var questionService = new QuestionService(new RepositoryFactory());

            questionService.AddQuestionsToTest(viewModel);

            return RedirectToAction("EditTest", "Test", new { id = testId });

        }



    }

}
