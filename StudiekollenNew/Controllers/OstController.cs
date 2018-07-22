using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using StudiekollenNew.Models;

namespace StudiekollenNew.Controllers
{
    public class OstController : Controller
    {
        private readonly Repository _repo;


        public OstController()
        {
            _repo =
                new Repository(new ApplicationDbContext());

        }

        // GET: Ost
        public ActionResult Index()
        {
            return View();
        }

        public OstTable GetOst(Models.OstTable model)
        {
            var x = new OstTable();
            try
            {
                x = _repo.GetOst("a");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return x;
        }
    }
}