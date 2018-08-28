using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
    // Sköter all CRUD-operatior för Testtabellen getemot databasen
    public class TestRepository
    {
        private ApplicationDbContext _context;

        // skapar databas connection genom konstruktorn
        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hämtar alla test i databasen
        public IEnumerable<Test> All()
        {
            return _context.Test.ToList();
        }

        // Lägger till ett test i databasen. 
        public void Add(Test testModel)
        {
            _context.Test.Add(testModel);
        }

        //TODO: Implement More Crud-OPERATIONS Here

    }
}