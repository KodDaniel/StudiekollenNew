using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;

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
    }

}
