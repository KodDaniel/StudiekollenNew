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
    [Authorize]
    public class QuestionController : Controller
    {
        public ActionResult DeleteQuestion(int questionId, int examId)
        {
            var questionService = new QuestionService(new RepositoryFactory());

            questionService.DeleteTest(questionId);

            return RedirectToAction("HandleExam", "Exam", new { examId = examId });
        }


        public ViewResult UpdateQuestion(int questionId, string examName, int examId)
        {        
            var questionService = new QuestionService(new RepositoryFactory());

            var questionModel = questionService.GetQuestion(questionId);
         
            var viewModel = new EditQuestionViewModel
            {     
                ExamId = examId,
                Name = examName,
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

            return RedirectToAction("HandleExam", "Exam", new {examId = tempModel.ExamId});
        }

        public ActionResult AddQuestionToExam(string examName, int examId)
        {
            var vievModel = new CreateExamViewModel()
            {
                ExamName = examName,
                ExamId = examId
            };

            TempData["viewModel"] = vievModel;


            return View(vievModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestionToExam(Question questionModel)
        {
            var tempModel = TempData["viewModel"] as CreateExamViewModel;

            TempData.Keep();

            if (!ModelState.IsValid)
            {
                var viewModel = new CreateExamViewModel
                {
                    ExamName = tempModel.ExamName,
                    Query = questionModel.Query,
                    Answer = questionModel.Answer
                };

                return View(viewModel);
            }

            var examId = tempModel.ExamId;

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.AddQuestion(examId, questionModel);

            return RedirectToAction("HandleExam", "Exam", new { examId = examId });

        }


    }

}
