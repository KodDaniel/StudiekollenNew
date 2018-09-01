using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
    public class UserRepository
    {

        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hämtar alla användare i databasen
        public IEnumerable<User> All()
        {
            return _context.Users.ToList();
        }

        // Get specific User
        public User GetSingleUser(string id)
        {
            return _context.Users.SingleOrDefault(c => c.Id == id);
        }
    }
}