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
    public class TestRepository
    {
        private ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _context.Test.ToList();
        }

        public void AddTest(Test testModel)
        {
            _context.Test.Add(testModel);
            _context.SaveChanges();
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

        public IEnumerable<Test> GetTestsForThisUserName(string userName)
        {
            return _context.Test.
                Include(a => a.User)
                .Where(a => a.User.UserName == userName)
                .ToList();
        }

        public Test GetSingleTestByTestId(int id)
        {
            return _context.Test.SingleOrDefault(c => c.Id == id);
        }

        public void RemoveTest(Test testModel)
        {
            _context.Test.Remove(testModel);
            _context.SaveChanges();
        }


    }
}

