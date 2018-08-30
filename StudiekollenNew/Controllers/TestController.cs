using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudiekollenNew.Models;
using StudiekollenNew.ViewModels;

namespace StudiekollenNew.Controllers
{
  
    public class TestController : Controller
    {
        // Vi vill inte ska skapa en instans av databasklassen i varje metod, lös detta!
        //private readonly ApplicationDbContext _context;

        public ViewResult NewTest()
        {
            var viewModel = new NewTestViewModel();

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult NewTest(Test testModel)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new NewTestViewModel();

                return View(viewModel);
            }
            else
            {
                TempData["testModel"] = testModel;

                return RedirectToAction("CreateTest");
            }
         
        }

        public ViewResult CreateTest()
        {
            
            var viewModel = new CreateTestViewModel();
            var testModel = TempData["testModel"] as Test;

            if (testModel is null)
            {
                viewModel.Name = "Placeholder";
            }
            else
            {
                viewModel.Name = testModel.Name; 
            }

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult CreateTest(Question questionModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CreateTestViewModel();
                return View(viewModel);
            }
            return RedirectToAction("CreateTest"); 
            
        }

    }
}






