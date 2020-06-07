using StackHackRegistrationWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public ActionResult Index([Bind(Exclude = "RegistrationID")]User user, HttpPostedFileBase img)
        {
            // test commit
            string message = "";
            bool status = false;

            // model validation
            if (ModelState.IsValid)
            {
                // genertae activation code.
                user.RegistrationID = Guid.NewGuid();
                if (img != null)
                {
                    user.IDCard = new byte[img.ContentLength];
                    img.InputStream.Read(user.IDCard, 0, img.ContentLength);
                }
                Session["info"] = user;
                using (StackHackDatabaseEntities dc = new StackHackDatabaseEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();
                    Console.WriteLine("Session = " + Session["info"].ToString());
                    // send email with registration id.
                    SendRegistrationEmail(user.Email, user.RegistrationID.ToString(), user.FullName);
                    message = "Registration is successfully done. Following is your Registration ID : " + user.RegistrationID;
                    status = true;
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


        [NonAction]
        public void SendRegistrationEmail(string email, string regID, string name)
        {
           
            var fromEmail = new MailAddress("youremailaddress", "Booking Details"); // replace youremailaddress with your actual email id.
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "*******"; /// replace here your actual password
            string subject = "Your Registration for the Gaming Night is confirmed!";
            string body = "<h2> Thank you " + name+"!"+
                           "<h3>Join us for some food, fun and games all night. Following is your registration ID  " + regID +
                          "</h3><h4>Hosted By: Bharti Singh</h4>" +
                          "<h4>Date: 08-06-2020</h4>" +
                          "<h4>Venue:My Place</h4>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            }) smtp.Send(message);
        }

        public ActionResult Preview()
        {
            return View();
        }
    }
}
