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
            bool status = false;

            // model validation
            if(ModelState.IsValid)
            {
                // genertae activation code.
                user.RegistrationID = Guid.NewGuid();

                // save to database
                using(StackHackDatabaseEntities dc = new StackHackDatabaseEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    // send email with registration id.

                }
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }

        // implement this.
        [NonAction]
        public void SendRegistrationEmail()
        {

        }
    }
}