using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.DataBase
{
    public class ContextSingelton
    {
        private static ApplicationDbContext _context;

        public static ApplicationDbContext GetContext()
        {
            // If we do not have an instance, create it (happens once)
            if (_context == null)
            {
                _context = new ApplicationDbContext();
            }
                return _context;       
        }
    }
}