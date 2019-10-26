using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DataBase;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
   // Denna metod sköter instansieringen av alla repositories
    public class RepositoryFactory
    {
        private static ApplicationDbContext context {get { return ContextSingelton.GetContext(); }}

     
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