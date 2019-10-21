using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using StudiekollenNew.ViewModels.ExamViewModels;
using StudiekollenNew.ViewModels.TestViewModels;
using CreateExamViewModel = StudiekollenNew.ViewModels.ExamViewModels.CreateExamViewModel;


namespace StudiekollenNew.Controllers
{

    [Authorize]
    public class ExamController : Controller
    {
        public ActionResult CreateExam(bool? regret)
        {
            if (regret == null)
            {
                return View(new CreateExamViewModel());
            }
            // Om bool inte är null har användaren ångrat sig (från examconfirmation.csthml)
            else
            {
                var sessionModel = Session["examViewModel"] as CreateExamViewModel;

                return View(sessionModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExam(CreateExamViewModel viewExamModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewExamModel);
            }

            Session["examViewModel"] = viewExamModel;


            return RedirectToAction("ExamConfirmation");

        }

        public ActionResult TimeInput()
        {
            return PartialView(new CreateExamViewModel());
        }

        public ActionResult ReminderDateInput()
        {
            
            return PartialView();
        }


        public ActionResult ExamConfirmation()
        {

            var sessionModel = Session["examViewModel"] as CreateExamViewModel;

            // Undersöker mina checkboxes
            string timeKeeping = (sessionModel.ExamTimeBool) ? "Ja" : "Nej";
            string sendReminder =  (sessionModel.ReminderDateBool) ? "Ja" : "Nej";
            string randomOrder = (sessionModel.RandomOrder)  ? "Ja" : "Nej";


            var confirmationModel = new ExamConfirmationViewModel
            {
                ExamName = sessionModel.ExamName,
                Timekeeping = timeKeeping,
                Duration = sessionModel.ExamTime.ToString(),
                SendReminder = sendReminder,
                ReminderDate = sessionModel.SendReminderDate.ToString(),
                Randomorder = randomOrder
            };

            return View(confirmationModel);

        }

        public ViewResult DisplayExams(string sortOrder)
        {
            var examService = new ExamService(new RepositoryFactory());

            var allExams = examService.GetAllExamsForThisUserId(User.Identity.GetUserId());

            ViewBag.UserName = User.Identity.GetUserName();

            // Notera användande av ternary operator
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.CreateDateSortParm = sortOrder == "CreateDate" ? "CreateDate_Desc" : "CreateDate";
            ViewBag.ChangeDateSortParm = sortOrder == "ChangeDate" ? "ChangeDate_Desc" : "ChangeDate";
            ViewBag.TimeKeepingSortParm = sortOrder == "TimeKeeping" ? "TimeKeeping_Desc" : "TimeKeeping";
            ViewBag.SendReminderSortParm = sortOrder == "SendReminder" ? "SendReminder_Desc" : "SendReminder";
            ViewBag.RandomOrderSortParm = sortOrder == "RandomOrder" ? "RandomOrder_Desc" : "RandomOrder";
            ViewBag.NumberOfQuestionsSortParm = sortOrder == "NumberOfQuestions" ? "NumberOfQuestions_Desc" : "NumberOfQuestions";


            // TO-DO: Lyfta ut sorteringen till repository genom att skicka predikat från switch. 
            switch (sortOrder)
            {
                case "Name_Desc":
                    allExams = allExams.OrderByDescending(s => s.ExamName);
                    break;
                case "CreateDate":
                    allExams = allExams.OrderBy(s => s.CreateDate);
                    break;
                case "CreateDate_Desc":
                    allExams = allExams.OrderByDescending(s => s.CreateDate);
                    break;
                case "ChangeDate":
                    allExams = allExams.OrderBy(s => s.ChangeDate);
                    break;
                case "ChangeDate_Desc":
                    allExams = allExams.OrderByDescending(s => s.ChangeDate);
                    break;
                case "TimeKeeping":
                    allExams =allExams.OrderBy(a => a.ExamTime);
                    break;
                case "TimeKeeping_Desc":
                    allExams = allExams.OrderByDescending(s => s.ExamTime);
                    break;
                case "SendReminder":
                    allExams = allExams.OrderBy(s => s.SendReminderDate);
                    break;
                case "SendReminder_Desc":
                    allExams = allExams.OrderByDescending(s => s.SendReminderDate);
                    break;
                case "RandomOrder":
                    allExams = allExams.OrderBy(s => s.RandomOrder);
                    break;
                case "RandomOrder_Desc":
                    allExams = allExams.OrderByDescending(s => s.RandomOrder);
                    break;
                   case "NumberOfQuestions":
                    allExams = allExams.OrderBy(s => s.Questions.Count);
                    break;
                case "NumberOfQuestions_Desc":
                    allExams = allExams.OrderByDescending(s => s.Questions.Count);
                    break;
                // Utgångsläget är sortering på skapelsedatum i fallande ordning
                default:
                    allExams = allExams.OrderByDescending(s => s.CreateDate);
                    break;

            }

            return View(allExams);

        }

        public ActionResult SaveExam()
        {
            var viewExamModel = Session["examViewModel"] as CreateExamViewModel;

            var examService = new ExamService(new RepositoryFactory());

            var examModel = new Exam
            {
                ExamName = viewExamModel.ExamName,
                ExamTime = viewExamModel.ExamTime,
                SendReminderDate = viewExamModel.SendReminderDate,
                RandomOrder = viewExamModel.RandomOrder
            };

            examService.AddExam(examModel, User.Identity.GetUserId());

            return RedirectToAction("DisplayExams");
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

            Session["viewModel"] = viewModel;


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateExam(Exam exam)
        {
            return Content("TO-DO!");
            //var sessionModel = Session["viewModel"] as UpdateExamViewModel;

            //if (!ModelState.IsValid)
            //{
            //    var viewModel = new UpdateExamViewModel
            //    {
            //        ExamName = sessionModel.ExamName,
            //    };

            //    return View(viewModel);
            //}

            //var examService = new ExamService(new RepositoryFactory());

            //examService.UpdateExam(exam, sessionModel.ExamId);

            //return RedirectToAction("HandleExam", "Exam", new { examId = sessionModel.ExamId });
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

        public ActionResult DeleteExam(int examId)
        {
            var testService = new ExamService(new RepositoryFactory());

            testService.DeleteExam(examId);

            return RedirectToAction("DisplayExams");
        }


       

        
    }
}



