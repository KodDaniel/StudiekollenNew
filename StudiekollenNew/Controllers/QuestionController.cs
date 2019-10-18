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

            questionService.DeleteQuestion(questionId);

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

            Session["viewModel"] = viewModel;


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(Question questionModel)
        {
            var sessionModel = Session["viewModel"] as EditQuestionViewModel;

            if (!ModelState.IsValid)
            {
                var viewModel = new EditQuestionViewModel
                {
                    Name = sessionModel.Name,
                    Query = sessionModel.Query,
                    Answer = sessionModel.Answer
                };

                return View(viewModel);
            }

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.UpdateQuestion(questionModel,sessionModel.QuestionId);

            return RedirectToAction("HandleExam", "Exam", new {examId = sessionModel.ExamId});
        }

        public ActionResult AddQuestionToExam(string examName, int examId)
        {
            var viewModel = new CreateExamViewModel()
            {
                ExamName = examName,
                ExamId = examId
            };

            Session["viewCreateModel"] = viewModel;


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestionToExam(Question questionModel)
        {
            var sessionModel = Session["viewCreateModel"] as CreateExamViewModel;

            if (!ModelState.IsValid)
            {
                var viewModel = new CreateExamViewModel
                {
                    ExamName = sessionModel.ExamName,
                    Query = questionModel.Query,
                    Answer = questionModel.Answer
                };

                return View(viewModel);
            }

            var examId = sessionModel.ExamId;

            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            questionService.AddQuestion(examId, questionModel);

            return RedirectToAction("HandleExam", "Exam", new { examId = examId });

        }


    }

}
