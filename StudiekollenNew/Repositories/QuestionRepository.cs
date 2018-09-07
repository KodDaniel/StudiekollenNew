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
            return _context.Question.Include(a => a.Test)
                .Where(a => a.TestId == id)
                .OrderBy(c => c.Id).ToList();
        }


        public void AddQuestionsToTest(int testId, Question questionModel)
        {
            questionModel.TestId = testId;

            _context.Question.Add(questionModel);

            _context.SaveChanges();
        }

      
        public void UpdateQuestion(Question questionModel, int questionId)
        {
            var currentQuestionModel = GetQuestion(questionId);

            currentQuestionModel.Query = questionModel.Query;

            currentQuestionModel.Answer = questionModel.Answer;

            currentQuestionModel.Test.ChangeDate = DateTime.Now;
            
            _context.SaveChanges();
        }


        public void RemoveQuestion(int id)
        {
            // Viss logik implementerad för att jag inte vill sätta att provet ändrats innan jag är säker på att delete-raden exekeveras. 
            // Försöka effektivisera detta.
            var questionModel = _context.Question
                .Single(a => a.Id == id);

            var belongsToTest = _context.Test
                .Single(a => a.Id == questionModel.TestId);

            _context.Question.Remove(questionModel);

            belongsToTest.ChangeDate = DateTime.Now;

            _context.SaveChanges();

        }

       

        //public int NumberOfQuestionsForThisTest(int testId)
        //{
        //    return _context.Question.Where(a => a.TestId == testId).Select(a => a.Id).Count();

        //}
    }
}