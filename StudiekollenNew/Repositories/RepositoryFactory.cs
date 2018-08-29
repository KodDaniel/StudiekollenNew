using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DataBase;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
   // This class handles instantiation of the repositories. (UserRepository, TestRepository och QuestionRepository). 
    public class RepositoryFactory
    {
        
        // Wrapper property to get a context instance
        private static ApplicationDbContext context {get { return ContextSingelton.GetContext(); }}


        // Retrieve a TestRepository instance
        public TestRepository GetTestRepository()
        {
            return new TestRepository(context);
        }

        public QuestionRepository GetQuestionRepository()
        {
            return new QuestionRepository(context);
        }

        // TODO: Insert UserRepository och QuestionRepository
    }
}