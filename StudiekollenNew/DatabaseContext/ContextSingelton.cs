using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.DataBase
{
    // Klass som hanterar livscykeln för DBContext i denna applikation
    public class ContextSingelton
    {
        private static ApplicationDbContext _context;

        // Om connection är null, returnera ny connection
        public static ApplicationDbContext GetContext()
        {
            return _context = _context ?? new ApplicationDbContext();
        }
    }
}