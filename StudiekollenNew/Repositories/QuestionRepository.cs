using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

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
        }


    }
}