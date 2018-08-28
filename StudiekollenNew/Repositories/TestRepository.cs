using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
    public class TestRepository
    {
        private ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Test> All()
        {
            return _context.Test.ToList();
        }

    }
}