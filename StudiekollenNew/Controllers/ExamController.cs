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
using CreateAndUpdateExamViewModel = StudiekollenNew.ViewModels.ExamViewModels.CreateAndUpdateExamViewModel;


namespace StudiekollenNew.Controllers
{

    [Authorize]
    public class ExamController : Controller
    {

        public ActionResult CreateExam(bool? regret)
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();

            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(new RepositoryFactory());
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            string errorMsgMeta = "Metadata gick inte att hämta";

            // Vill att sidan ska ladda även om metadata inte lyckas hämtas
            if (meta == null)
            {
                ViewBag.Title = errorMsgMeta;
                ViewBag.Description = errorMsgMeta;
                ViewBag.Keywords = errorMsgMeta;
            }
            else
            {
                // Undersöker därför också respektive property (Om null sätt errormsg)
                ViewBag.Title = meta.Title ?? errorMsgMeta;
                ViewBag.Description= meta.MetaDescription ?? errorMsgMeta;
                ViewBag.Keywords = meta.MetaKeyWords ?? errorMsgMeta;
            }

            if (regret == null)
            {
                return View(new CreateAndUpdateExamViewModel());
            }

            // Om bool inte är null har användaren ångrat sig (från examconfirmation.csthml)
            var sessionModel = Session["examViewModel"] as CreateAndUpdateExamViewModel;

                return View(sessionModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExam(CreateAndUpdateExamViewModel viewExamModel)
        {
            // Ser till att ett ett ifyllt formulär nollställs om tillhörande checkbox klickas ur
            viewExamModel.ExamTime = (!viewExamModel.ExamTimeBool) ? null : viewExamModel.ExamTime;
            viewExamModel.SendReminderDate = (!viewExamModel.ReminderDateBool) ? null : viewExamModel.SendReminderDate;

            if (!ModelState.IsValid)
            {
                return View(viewExamModel);
            }

            if (viewExamModel.ExamTimeBool)
            {
                if (viewExamModel.ExamTime == null)
                {
                    ModelState.AddModelError("ExamTime", "Du måste fylla i en provtid");

                    return View(viewExamModel);
                }

                // Kontrollerar giltig provtid
                if (viewExamModel.ExamTime < new TimeSpan(0, 0, 1, 0))
                {
                    ModelState.AddModelError("ExamTime", "Ett prov måste pågå under minst en minut");

                    return View(viewExamModel);
                }

            }

            if (viewExamModel.ReminderDateBool)
            {
                if (viewExamModel.SendReminderDate == null)
                {
                    ModelState.AddModelError("SendReminderDate", "Du måste fylla i ett datum för mejlpåminnelse");
                    return View(viewExamModel);
                }

                if (viewExamModel.SendReminderDate <= DateTime.Now)
                {
                    ModelState.AddModelError("SendReminderDate", "Du måste ange ett datum som inte redan varit");
                    return View(viewExamModel);
                }

            }

            Session["examViewModel"] = viewExamModel;


            return RedirectToAction("ExamConfirmation");

        }

        public ActionResult TimeInput()
        {
            return PartialView("_TimeInput");
        }

        public ActionResult ReminderDateInput()
        {
            return PartialView("_ReminderDateInput");
        }


        public ActionResult ExamConfirmation()
        {
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();

            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(new RepositoryFactory());
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            string errorMsgMeta = "Metadata gick inte att hämta";

            // Vill att sidan ska ladda även om metadata inte lyckas hämtas
            if (meta == null)
            {
                ViewBag.Title = errorMsgMeta;
                ViewBag.Description = errorMsgMeta;
                ViewBag.Keywords = errorMsgMeta;
            }
            else
            {
                // Undersöker därför också respektive property (Om null sätt errormsg)
                ViewBag.Title = meta.Title ?? errorMsgMeta;
                ViewBag.Description = meta.MetaDescription ?? errorMsgMeta;
                ViewBag.Keywords = meta.MetaKeyWords ?? errorMsgMeta;
            }

            var sessionModel = Session["examViewModel"] as CreateAndUpdateExamViewModel;

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
            var repoFactory = new RepositoryFactory();

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(repoFactory);
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            string errorMsgMeta = "Metadata gick inte att hämta";

            // Vill att sidan ska ladda även om metadata inte lyckas hämtas
            if (meta == null)
            {
                ViewBag.Title = errorMsgMeta;
                ViewBag.Description = errorMsgMeta;
                ViewBag.Keywords = errorMsgMeta;
            }
            else
            {
                // Undersöker därför också respektive property (Om null sätt errormsg)
                ViewBag.Title = meta.Title ?? errorMsgMeta;
                ViewBag.Description = meta.MetaDescription ?? errorMsgMeta;
                ViewBag.Keywords = meta.MetaKeyWords ?? errorMsgMeta;
            }

            var examService = new ExamService(repoFactory);

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


            // Förbättringspotential: Skicka LINQ-queries till service/repository och hämta in resultat
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
            var viewExamModel = Session["examViewModel"] as CreateAndUpdateExamViewModel;

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
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var repoFactory = new RepositoryFactory();

            var metaService = new MetaTagService(repoFactory);
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            string errorMsgMeta = "Metadata gick inte att hämta";

            // Vill att sidan ska ladda även om metadata inte lyckas hämtas
            if (meta == null)
            {
                ViewBag.Title = errorMsgMeta;
                ViewBag.Description = errorMsgMeta;
                ViewBag.Keywords = errorMsgMeta;
            }
            else
            {
                // Undersöker därför också respektive property (Om null sätt errormsg)
                ViewBag.Title = meta.Title ?? errorMsgMeta;
                ViewBag.Description = meta.MetaDescription ?? errorMsgMeta;
                ViewBag.Keywords = meta.MetaKeyWords ?? errorMsgMeta;
            }
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
            var repoFactory = new RepositoryFactory();

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(repoFactory);
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            string errorMsgMeta = "Metadata gick inte att hämta";

            // Vill att sidan ska ladda även om metadata inte lyckas hämtas
            if (meta == null)
            {
                ViewBag.Title = errorMsgMeta;
                ViewBag.Description = errorMsgMeta;
                ViewBag.Keywords = errorMsgMeta;
            }
            else
            {
                // Undersöker därför också respektive property (Om null sätt errormsg)
                ViewBag.Title = meta.Title ?? errorMsgMeta;
                ViewBag.Description = meta.MetaDescription ?? errorMsgMeta;
                ViewBag.Keywords = meta.MetaKeyWords ?? errorMsgMeta;
            }

            var examService = new ExamService(repoFactory);

            var examToUpdate = examService.GetSingleExam(examId);

            var examSettings = new CreateAndUpdateExamViewModel
            {
                // Två första raderna sätter checkboxar utifrån villkor
                ExamTimeBool = (examToUpdate.ExamTime != null),
                ReminderDateBool = (examToUpdate.SendReminderDate != null),
                RandomOrder = examToUpdate.RandomOrder,
                ExamName = examToUpdate.ExamName,
                ExamTime = examToUpdate.ExamTime,
                SendReminderDate = examToUpdate.SendReminderDate,
                ExamId = examId
            };

            Session["ExamId"] = examId;

            return View(examSettings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateExam(CreateAndUpdateExamViewModel viewExamModel)
        {

            viewExamModel.ExamTime = (!viewExamModel.ExamTimeBool) ? null : viewExamModel.ExamTime;
            viewExamModel.SendReminderDate = (!viewExamModel.ReminderDateBool) ? null : viewExamModel.SendReminderDate;

            if (!ModelState.IsValid)
            {
                return View(viewExamModel);
            }

            if (viewExamModel.ExamTimeBool)
            {
                if (viewExamModel.ExamTime == null)
                {
                    ModelState.AddModelError("ExamTime", "Du måste fylla i en provtid");

                    return View(viewExamModel);
                }

                if (viewExamModel.ExamTime < new TimeSpan(0, 0, 1, 0))
                {
                    ModelState.AddModelError("ExamTime", "Ett prov måste pågå under minst en minut");

                    return View(viewExamModel);
                }

            }

            if (viewExamModel.ReminderDateBool)
            {
                if (viewExamModel.SendReminderDate == null)
                {
                    ModelState.AddModelError("SendReminderDate", "Du måste fylla i ett datum för mejlpåminnelse");
                    return View(viewExamModel);
                }

                if (viewExamModel.SendReminderDate <= DateTime.Now)
                {
                    ModelState.AddModelError("SendReminderDate", "Du måste ange ett datum som inte redan varit");
                    return View(viewExamModel);
                }

            }
            var examService = new ExamService(new RepositoryFactory());

            int examId = (int)Session["examId"];

            var exam = new Exam
            {
                ExamName = viewExamModel.ExamName,
                SendReminderDate = viewExamModel.SendReminderDate,
                ExamTime = viewExamModel.ExamTime,
                RandomOrder = viewExamModel.RandomOrder,
                ExamId = examId
            };

            examService.UpdateExam(exam,examId);

            viewExamModel.ExamId = examId;

            return View(viewExamModel);
        }

        public ActionResult SearchForExam()
        {
            var repoFactory = new RepositoryFactory();

            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string metaDataUrl = "/" + controllerName + "/" + actionName;

            var metaService = new MetaTagService(repoFactory);
            var meta = metaService.GetPageMetaTags(metaDataUrl);

            string errorMsgMeta = "Metadata gick inte att hämta";

            // Vill att sidan ska ladda även om metadata inte lyckas hämtas
            if (meta == null)
            {
                ViewBag.Title = errorMsgMeta;
                ViewBag.Description = errorMsgMeta;
                ViewBag.Keywords = errorMsgMeta;
            }
            else
            {
                // Undersöker därför också respektive property (Om null sätt errormsg)
                ViewBag.Title = meta.Title ?? errorMsgMeta;
                ViewBag.Description = meta.MetaDescription ?? errorMsgMeta;
                ViewBag.Keywords = meta.MetaKeyWords ?? errorMsgMeta;
            }

            var testService = new ExamService(repoFactory);

            var allExamsForThisUser = testService.GetAllExamsForThisUserId(User.Identity.GetUserId());

            if (!allExamsForThisUser.Any())
            {
                return View("_NoExams");
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



