using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.DataBase
{
    // Class which handels the Life-cycle of DbContext (in this Application) 
    public class ContextSingelton
    {
        private static ApplicationDbContext _context;

        // Testar Null operator (Om connection är null, returnera ny connection)
        public static ApplicationDbContext GetContext()
        {
            return _context ?? (_context = new ApplicationDbContext());
        }
    }
}