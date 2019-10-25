using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DataBase;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
   // This class handles instantiation of the repositories. (UserRepository, ExamRepository och QuestionRepository). 
    public class RepositoryFactory
    {
        
        // Wrapper property to get a context instance
        private static ApplicationDbContext context {get { return ContextSingelton.GetContext(); }}


        // Retrieve a ExamRepository instance
        public ExamRepository GetExamRepository()
        {
            return new ExamRepository(context);
        }

        public QuestionRepository GetQuestionRepository()
        {
            return new QuestionRepository(context);
        }

        public UserRepository GetUserRepository()
        {
            return new UserRepository(context);
        }

        public MetaTagRepository GetMetaTagRepository()
        {
            return new MetaTagRepository(context);
        }


    }
}