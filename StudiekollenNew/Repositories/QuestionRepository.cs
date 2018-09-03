using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.ViewModels;
using NetPipeStyleUriParser = System.NetPipeStyleUriParser;
namespace StudiekollenNew.Repositories
{
    public class QuestionRepository
    {
        private ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Question GetSingleQuestionModelByQuestionId(int id)
        {
            return _context.Question.Find(id);
        }

        public void UpdateQuestion(Question questionModel, int questionId)
        {
            var currentQuestionModel = GetSingleQuestionModelByQuestionId(questionId);
            currentQuestionModel.Query = questionModel.Query;
            currentQuestionModel.Answer = questionModel.Answer;
            currentQuestionModel.Test.ChangeDate = DateTime.Now;;
            _context.SaveChanges();

        }

        // Lägger till ett Question i databasen. 
        public void AddQuestionsToTest(int testId, Question questionModel)
        {
            questionModel.TestId = testId;
            _context.Question.Add(questionModel);
            _context.SaveChanges();
        }

        public void RemoveQuestionFromTest(int id)
        {
            // Viss logik implementerad för att jag inte vill sätta att provet ändrats innan jag är säker på att delete-raden exekeveras. 
            // Försöka effektivisera detta.
            var questionModel = _context.Question.Single(a => a.Id == id);
            var belongsToTest = _context.Test.Single(a => a.Id == questionModel.TestId);
            _context.Question.Remove(questionModel);
            belongsToTest.ChangeDate = DateTime.Now;
            _context.SaveChanges();

        }

        public List<Question> AllQuestionModelsByTestId(int id)
        {
            return _context.Question.Include(a => a.Test)
                .Where(a => a.TestId == id).OrderByDescending(c=>c.Id).ToList();
        }

    }
}