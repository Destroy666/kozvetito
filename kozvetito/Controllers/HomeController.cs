using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using kozvetito.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace kozvetito.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mail()
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.To.Add("razor2060@gmail.com");
                mail.From = new MailAddress("noreply@exclusiveteam.net");
                mail.Subject = "Exclusive Team: Sikeres vásárlás";
                string Body =
                    "<h3>Teszt</h3><p>Teszt üzenet</p><p>Exclusive Team</p>";
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "relay-hosting.secureserver.net";
                smtp.Port = 25;
                smtp.Credentials = new NetworkCredential("noreply@exclusiveteam.net", "admin666");
                smtp.EnableSsl = false;
                smtp.Send(mail);
            }
            catch (Exception)
            {
                
                throw;
            }
            return View();
        }
    }
}