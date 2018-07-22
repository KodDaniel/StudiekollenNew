////using System;
////using System.Collections.Generic;
////using System.Data.Entity;
////using System.Linq;
////using System.Web;

////namespace StudiekollenNew.Models
////{
    //public class Repository
    //{
    //    public ApplicationDbContext DbContext { get; set; }

    //    public Repository(ApplicationDbContext dbContext)
    //    {
    //        DbContext = dbContext;
    //    }

    //    public OstTable GetOst(string ostId)
    //    {
    //        var ost = DbContext.OstTable.Where(x => x.ID == ostId).SingleOrDefault();
    //        return ost;

    //    }

    //    public QuestionTable CreateQuestion(string question)
    //    {
    //       DbContext.
    //        context.SaveChanges();

    //    }

//    }
//}