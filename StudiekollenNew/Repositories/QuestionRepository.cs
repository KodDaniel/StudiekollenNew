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

        // Lägger till ett Question i databasen. 
        public void AddQuestionsToTest(int testId, Question questionModel)
        {
            questionModel.TestId = testId;
            _context.Question.Add(questionModel);
            _context.SaveChanges();
        }

        public void RemoveQuestionFromTest(Dictionary<string, string> dictionary, string key)
        {
            dictionary.Remove(key);
            _context.SaveChanges();

        }

        public List<Question> AllQuestionModelsByTestId(int id)
        {
            return _context.Question.Include(a => a.Test)
                .Where(a => a.TestId == id).ToList();
        }

    }
}