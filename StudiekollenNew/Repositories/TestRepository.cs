using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using NetPipeStyleUriParser = System.NetPipeStyleUriParser;

namespace StudiekollenNew.Repositories
{
    public class TestRepository
    {
        private ApplicationDbContext _context;

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
        public void AddTest(Test testModel)
        {
            _context.Test.Add(testModel);
        }

        public int GetMostRecentTestId(string currentUserId)
        {
            // Substitut för Last-operator. Tänk på att du ej behöver EagerLoda med "Include" eftersom som du ju här rör dig i en och samma tabell.
            return _context.Test
                .OrderByDescending(c => c.Id)
                .First(c => c.UserId == currentUserId).Id;
        }

        public string GetMostRecentTestName(string currentUserId)
        {
            // Substitut för Last-operator. Tänk på att du ej behöver EagerLoda med "Include" eftersom som du ju här rör dig i en och samma tabell.
            return _context.Test
                .OrderByDescending(c => c.Id)
                .First(c => c.UserId == currentUserId).Name;
        }

        public string FindTest()
        {
            throw new NotImplementedException();
        }






        //TODO: Implement More Crud-OPERATIONS Here

    }
}