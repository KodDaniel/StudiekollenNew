﻿using System;
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

        public static ApplicationDbContext GetContext()
        {
            if (_context == null)
            {
                _context = new ApplicationDbContext();
            }
                return _context;       
        }
    }
}