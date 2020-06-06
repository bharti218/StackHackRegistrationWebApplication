using StackHackRegistrationWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackHackRegistrationWebApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // Registration Action
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        // Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            // test commit
            string message = "";
            return View(user);
        }
    }
}