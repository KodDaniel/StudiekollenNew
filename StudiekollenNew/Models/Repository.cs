using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class Repository
    {
        private ApplicationDbContext DbContext { get; }

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public OstTable GetOst(string ostId)
        {
            var ost = DbContext.OstTable.Where(x => x.ID == ostId).SingleOrDefault();
            return ost;

        }

    }
}