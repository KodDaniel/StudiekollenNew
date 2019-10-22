using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.ViewModels;
namespace StudiekollenNew.Repositories
{
    public class QuestionRepository
    {
        private ApplicationDbContext _context;


        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Question GetQuestion(int id)
        {
            return _context.Question.Find(id);
        }


        public List<Question> GetAllQuestions(int id)
        {
            return _context.Question.Include(a => a.Exam)
                .Where(a => a.ExamId == id)
                .OrderBy(c => c.QuestionId).ToList();
        }


        public void AddQuestion(int examId, Question questionModel)
        {
            questionModel.ExamId = examId;

            _context.Question.Add(questionModel);

            var belongsToExam = _context.Exam
                .Single(a => a.ExamId == questionModel.ExamId);

            belongsToExam.ChangeDate = DateTime.Now;

            _context.SaveChanges();
        }

      
        public void UpdateQuestion(Question questionModel, int questionId)
        {
            var currentQuestionModel = GetQuestion(questionId);

            currentQuestionModel.Query = questionModel.Query;

            currentQuestionModel.Answer = questionModel.Answer;

            currentQuestionModel.Exam.ChangeDate = DateTime.Now;
            
            _context.SaveChanges();
        }


        public void DeleteQuestion(int id)
        {
            // Viss logik implementerad för att jag inte vill sätta att provet ändrats innan jag är säker på att delete-raden exekeveras. 
            // Försöka effektivisera detta.
            var questionModel = _context.Question
                .Single(a => a.QuestionId == id);

            var belongsToExam = _context.Exam
                .Single(a => a.ExamId == questionModel.ExamId);

            _context.Question.Remove(questionModel);

            belongsToExam.ChangeDate = DateTime.Now;

            _context.SaveChanges();

        }

    }
}