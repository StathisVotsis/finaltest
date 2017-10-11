using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using finaltest.Models;

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
            return View();
        }
    }
}