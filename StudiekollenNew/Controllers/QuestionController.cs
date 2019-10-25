using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;
using StudiekollenNew.ViewModels;
using StudiekollenNew.ViewModels.ExamViewModels;
using StudiekollenNew.ViewModels.QuestionsViewModels;

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
            var repoFactory = new RepositoryFactory();

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(repoFactory);
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            ViewBag.Title = meta.Title;
            ViewBag.Description = meta.MetaDescription;
            ViewBag.Keywords = meta.MetaKeyWords;

            var questionService = new QuestionService(repoFactory);

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

            var questionService = new QuestionService(new RepositoryFactory());

            questionService.UpdateQuestion(questionModel,sessionModel.QuestionId);

            return RedirectToAction("HandleExam", "Exam", new {examId = sessionModel.ExamId});
        }

        public ActionResult AddQuestionToExam(string examName, int examId)
        {
            var repoFactory = new RepositoryFactory();

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(repoFactory);
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            ViewBag.Title = meta.Title;
            ViewBag.Description = meta.MetaDescription;
            ViewBag.Keywords = meta.MetaKeyWords;

            var viewModel = new AddQuestionViewModel()
            {
                ExamName = examName,
                ExamId = examId
            };

            Session["viewAddQuestionModel"] = viewModel;


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestionToExam(Question questionModel)
        {
            var sessionModel = Session["viewAddQuestionModel"] as AddQuestionViewModel;

            if (!ModelState.IsValid)
            {
                var viewModel = new AddQuestionViewModel()
                {
                    ExamName = sessionModel.ExamName,
                    Query = questionModel.Query,
                    Answer = questionModel.Answer
                };

                return View(viewModel);
            }

            var examId = sessionModel.ExamId;

            var questionService = new QuestionService(new RepositoryFactory());

            questionService.AddQuestion(examId, questionModel);


            return RedirectToAction("HandleExam", "Exam", new { examId = examId });

        }


    }

}
