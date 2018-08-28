using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DataBase;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
    public class RepositoryFactory
    {
        private static ApplicationDbContext context
        {
            get { return ContextSingelton.GetContext(); }
        }

        public TestRepository GetTestRepository()
        {
            return new TestRepository(context);
        }

        // Insert UserRepository och QuestionRepository
    }
}