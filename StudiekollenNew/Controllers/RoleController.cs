using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudiekollenNew.Models;

namespace StudiekollenNew.Controllers
{
    public partial class ManageController
    {
        public ActionResult Roles()
        {
            var roles = RoleManager.Roles.ToList();
        
            return View(roles.Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }));
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleViewModel model)
        {
            // We werify the model
            // We verify if the role already exists using the RoleManager
            if (RoleManager.RoleExists(model.Name))
            {
                //We informing the user that the role already exists
            }

            var newRole = new IdentityRole(model.Name);
            var result = RoleManager.Create(newRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }
            //Here returns the model in sight
         

            return View(model);
        }

        public ActionResult AddRoleToUser()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult AddRoleToUser(string user, string role)
        {
            // Get the User
            // Get the Role

            var _user = UserManager.FindByEmail(user);
            var _role = RoleManager.FindByName(role);

            //The user already has the role
            // Does the role already exists?
            var result = UserManager.AddToRole(_user.Id, role);


            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }

            return View();
        }
    }
}