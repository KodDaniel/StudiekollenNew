using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ViewResult EditQuestion(int questionId, string testName)
        {
            var repoFactory = new RepositoryFactory();
            var questionService = new QuestionService(repoFactory);
            var questionModel = questionService.GetSingleQuestionModelByQuestionId(questionId);
            var viewmodel = new EditQuestionViewModel
            {
                Name = testName,
                Query = questionModel.Query,
                Answer = questionModel.Answer
            };
            return View(viewmodel);
        }
    }

}
