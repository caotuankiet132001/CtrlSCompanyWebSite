using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CtrlS.Models;
using System.Net.Mail;
using System.Net.Mime;
using System;

namespace CtrlS.Controllers
{
    public class HomeController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();
        public ActionResult Index()
        {
            var @event = db.Events.Where(m=>m.Status != 0).ToList();
            return View(@event);
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Infomation()
        {
            return View();
        }
        private List<Client> GetClients()
        {
            List<Client> clients = db.Clients.Where(m => m.Status != 0).ToList();
            return clients;
        }
        private List<Talent> GetTalents()
        {
            List<Talent> talents = db.Talents.Where(m => m.Status != 0).ToList();
            return talents;
        }
        public ActionResult About()
        {
            dynamic dy = new ExpandoObject();
            dy.Clients = GetClients();
            dy.Talents = GetTalents();
            return View(dy);
        }


        public ActionResult Contact()
        {

            SmtpClient smtp = new SmtpClient("smtp.mailtrap.io", 2525);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("SMTP_USERNAME", "SMTP_PASSWORD");

            String from = "caotuankiet132001@gmail.com";
            String to = "ctrlscompany@gmail.com";
            String subject = "Pics from the royal wedding";
            String messageBody = "Charles, sending you a quick sneak peek of the pictures" +
            "we took at the last royal wedding. ";
            MailMessage message = new MailMessage(from, to, subject, messageBody);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
              $"{messageBody} <br> <img src=\"cid:Wedding\">",
              null,
              "text/html"
            );

            LinkedResource LinkedImage = new LinkedResource("./bestpictureever.jpeg");
            LinkedImage.ContentId = "Wedding";

            htmlView.LinkedResources.Add(LinkedImage);
            message.AlternateViews.Add(htmlView);

            //try
            //{
            //    smtp.Send(message);
            //}
            //catch (SmtpException ex)
            //{
            //    return ex.ToString();
            //}

            return View();
        }
    }
}