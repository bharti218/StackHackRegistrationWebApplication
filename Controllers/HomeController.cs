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
                // save to database
                using (StackHackDatabaseEntities dc = new StackHackDatabaseEntities())
                {
                   
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    // send email with registration id.
                    SendRegistrationEmail(user.Email, user.RegistrationID.ToString());
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

        // implement this.
        [NonAction]
        public void SendRegistrationEmail(string email, string regID)
        {
            var fromEmail = new MailAddress("bhartisingh5492@gmail.com", "Booking Details");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "p@r!huma!21";
            string subject = "Your Registration for the event is confirmed!";
            string body = "<br/><br/>You are successfully registered for the event following is your registration ID  " + regID;
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

    }
}
