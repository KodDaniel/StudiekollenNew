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

        // Lägger till ett Question i databasen. 
        public void AddQuestionsToTest(CreateTestViewModel viewModel)
        {
           
            var questionModel = new Question
            {
                TestId = viewModel.Id,
                Query = viewModel.Query,
                Answer = viewModel.Answer

            };
            _context.Question.Add(questionModel);

            _context.SaveChanges();
        }

        public Question GetSingleQuestionModelByQuestionId(int id)
        {
            return _context.Question.Find(id);
        }

        public void UpdateQuestion(EditQuestionViewModel viewModel, int questionId)
        {
            var currentQuestionModel = GetSingleQuestionModelByQuestionId(questionId);

            currentQuestionModel.Query = viewModel.Query;

            currentQuestionModel.Answer = viewModel.Answer;

            currentQuestionModel.Test.ChangeDate = DateTime.Now;
            
            _context.SaveChanges();

        }

        

        public void RemoveQuestionFromTest(int id)
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

        public List<Question> AllQuestionModelsByTestId(int id)
        {
            return _context.Question.Include(a => a.Test)
                .Where(a => a.TestId == id)
                .OrderBy(c => c.Id).ToList();
        }

        //public int NumberOfQuestionsForThisTest(int testId)
        //{
        //    return _context.Question.Where(a => a.TestId == testId).Select(a => a.Id).Count();

        //}
    }
}