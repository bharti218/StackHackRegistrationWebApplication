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


        // Registration POST action
        [HttpGet]
        public ActionResult Index()
        {
            

            return View();
        }
    }
}