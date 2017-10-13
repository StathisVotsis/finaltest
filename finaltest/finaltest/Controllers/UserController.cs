using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using finaltest.Models;
using System.Web.Security;

namespace finaltest.Controllers
{
    
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Registration(User userModel)
        {
            using (DbModels dbModel = new DbModels())
            {
                if(dbModel.Users.Any(x =>x.Username == userModel.Username))
                {
                    ViewBag.DuplicateMessage = "User already exists";
                    return View("Registration", new User());
                }
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }

            ModelState.Clear();
            ViewBag.SuccessMessage = "You have registered as" +" "+userModel.Username;
            return View("Registration", new User());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userModel,string returnUrl)
        {
            DbModels dbModel = new DbModels();
            var data = dbModel.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).First();
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.Username, false);
                if(Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && returnUrl.StartsWith("//") && returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }

                else
                {
                    return RedirectToAction("Index","Home");
                }
            }

            else
            {
                ModelState.AddModelError("","Invalid credentials");
                return View();
            }
        }


    }
}