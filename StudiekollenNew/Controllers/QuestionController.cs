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
    // Finns ingen validering i klassen 
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
        public ActionResult EditQuestion(Question questionModel)
        {
            var viewmodel = TempData["viewModel"] as EditQuestionViewModel;

            if (!ModelState.IsValid)
            {
                var viewModel = new EditQuestionViewModel
                {
                    Name = viewmodel.Name,
                    Query = viewmodel.Query,
                    Answer = viewmodel.Answer
                };

                return View(viewModel);
            }
                
            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.UpdateQuestion(questionModel,viewmodel.QuestionId);

            return RedirectToAction("EditTest", "Test", new { id = viewmodel.TestId});
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
        public ActionResult AddQuestionToTest(Question questionModel)
        {

            var viewModel = TempData["viewModel"] as CreateTestViewModel;

            var testId = viewModel.TestId;

            questionModel.TestId = testId;

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.AddQuestionsToTest(testId, questionModel);

            return RedirectToAction("EditTest", "Test", new { id = testId });

        }


    }

}
