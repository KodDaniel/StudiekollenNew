using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using StudiekollenNew.DataBase;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;
using StudiekollenNew.ViewModels;
using StudiekollenNew.ViewModels.TestViewModels;

namespace StudiekollenNew.Controllers
{

    [Authorize]
    public class ExamController : Controller
    {
        public ViewResult NewExam()
        {
            var viewModel = new NewExamViewModel();

            return View(viewModel); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewExam(Exam examModel)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new NewExamViewModel
                {
                    ExamName = examModel.ExamName
                };

                return View(viewModel);
            }

            var userId = User.Identity.GetUserId();

            var examService = new ExamService(new RepositoryFactory());

            examService.AddExam(examModel,userId);

            return RedirectToAction("CreateExam", new {eName = examModel.ExamName});
        
       }

        public ActionResult CreateExam(string examName)
        {

            var viewModel = new CreateExamViewModel
            {
                ExamName = examName
            };
      
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExam(Question questionModel)
        {
            var repoFactory = new RepositoryFactory();

            var examService = new ExamService(repoFactory);
            
            var userId = User.Identity.GetUserId();

            var recentExamName = examService.GetMostRecentExam(userId).ExamName;

            if (!ModelState.IsValid)
            {
                var viewModel = new CreateExamViewModel
                {
                    ExamName = recentExamName,
                    Query = questionModel.Query,
                    Answer = questionModel.Answer
                };

                return View(viewModel);
            }

            var ExamId = examService.GetMostRecentExam(userId).ExamId;

            var questionService = new QuestionService(repoFactory);

            questionService.AddQuestion(ExamId,questionModel);

            return RedirectToAction("CreateExam", new {examName = recentExamName});
        }

        public ViewResult UpdateExam(int examId)
        {
            var examService = new ExamService(new RepositoryFactory());

            var exam = examService.GetSingleExam(examId);

            examService.UpdateExam(exam,examId);

            var viewModel = new UpdateExamViewModel
            {
                ExamName = exam.ExamName,
                ExamId = examId
                
            };

            TempData["viewModel"] = viewModel;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateExam(Exam exam)
        {
            var tempModel = TempData["viewModel"] as UpdateExamViewModel;

            TempData.Keep();

            if (!ModelState.IsValid)
            {
                var viewModel = new UpdateExamViewModel
                {
                    ExamName = tempModel.ExamName,
                };

                return View(viewModel);
            }

            var examService = new ExamService(new RepositoryFactory());

            examService.UpdateExam(exam, tempModel.ExamId);

            return RedirectToAction("HandleExam", "Exam", new { examId = tempModel.ExamId });
        }


        public ActionResult SearchForExam()
        {
            var testService = new ExamService(new RepositoryFactory());

            

            var allExamsForThisUser = testService.GetAllExamsForThisUserId(User.Identity.GetUserId());

            if (!allExamsForThisUser.Any())
            {
                return View("_partialDetails");
            }

            var vievModel = new SearchExamViewModel
            {
                AllExams = allExamsForThisUser
            };

            return View(vievModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchForExam(string id)
        {
            return RedirectToAction("HandleExam", new { examId = id });

        }


        public ViewResult DetailsExam()
        {
            var examService = new ExamService(new RepositoryFactory());

            var allExams = examService.GetAllExamsForThisUserId(User.Identity.GetUserId());
       
            var viewmodel = new SearchExamViewModel
            {
                AllExams = allExams
            };

            return View(viewmodel);
        }

        public ActionResult DeleteExam(int examId)
        {
            var repoFactory = new RepositoryFactory();

            var testService = new ExamService(repoFactory);

            testService.DeleteExam(examId);

            return RedirectToAction("DetailsExam");
        }

        public ViewResult HandleExam(int examId)
        {
            var repoFactory = new RepositoryFactory();

            var questionService = new QuestionService(repoFactory);

            var examService = new ExamService(repoFactory);

            var examName = examService.GetSingleExam(examId).ExamName;

            var questionModels = questionService.GetAllQuestions(examId);

            var viewModel = new HandleExamViewModel
            {
                ExamId = examId,
                ExamName = examName,
                QuestionsModels = questionModels
            };

            return View(viewModel);
        }

        
    }
}



