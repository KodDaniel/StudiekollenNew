using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudiekollenNew.Models;

namespace StudiekollenNew.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

      
      

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,CreateDate,ChangeDate")] TestTable testTable)
        {
            if (ModelState.IsValid)
            {
                testTable.UserId = User.Identity.GetUserId();
           
                db.TestTable.Add(testTable);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", testTable.UserId);
            return View(testTable);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
