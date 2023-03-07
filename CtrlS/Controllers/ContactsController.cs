using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CtrlS.Helper;
using CtrlS.Models;
using System.Net.Mail;
using System.Net.Mime;

namespace CtrlS.Controllers
{
    public class ContactsController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();

        // GET: Contacts
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            return View(db.Contacts.OrderByDescending(m=>m.DateTime).ToList());
        }

        // GET: Contacts/Details/5
        [CustomAuthorize("Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        //[CustomAuthorize("Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[CustomAuthorize("Admin")]
        //[HttpPost]
        //public ActionResult Create(string Name, string Message, string Subject, string PhoneNumber, string Email)
        //{
        //    Contact contact = new Contact();
        //    contact.Name = Name;
        //    contact.Message = Message;
        //    contact.Subject = Subject;
        //    contact.PhoneNumber = PhoneNumber;
        //    contact.Email = Email;
        //    contact.DateTime = DateTime.Now;
        //    contact.Status = 0;
        //    db.Contacts.Add(contact);
        //    db.SaveChanges();
        //    TempData["contact"] = "<script>alert('CtrlS đã nhận thông tin của quý khách');</script>";
        //    return RedirectToAction("Index", "Home");
        //}
        [HttpPost]
        public ActionResult Create(string Name, string Message, string Subject, string PhoneNumber, string Email)
        {
            //Gửi mail tới CEO
            string fromMail = "caotuankiet132001@gmail.com";
            string fromPassword = "sgexvnvsnzmaxivs";
            MailMessage message = new MailMessage();
            message.Subject = Subject;
            message.From = new MailAddress(fromMail);
            string mess = "Nội dung - " + Message + " - Email - " + Email + " - Số Điện Thoại - " + PhoneNumber;
            message.Body = mess;
            message.To.Add(new MailAddress("ctrlscompany@gmail.com"));
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
            //Lưu thông tin phản hồi về đb
            Contact contact = new Contact();
            contact.Name = Name;
            contact.Message = Message;
            contact.Subject = Subject;
            contact.PhoneNumber = PhoneNumber;
            contact.Email = Email;
            contact.DateTime = DateTime.Now;
            contact.Status = 0;
            db.Contacts.Add(contact);
            db.SaveChanges();
            TempData["contact"] = "<script>alert('CtrlS đã nhận thông tin của quý khách');</script>";
            return RedirectToAction("Index", "Home");
        }
        // GET: Contacts/Edit/5
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Subject,Message,PhoneNumber,DateTime,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [CustomAuthorize("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [CustomAuthorize("Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [CustomAuthorize("Admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [CustomAuthorize("Admin")]
        public ActionResult Status(int id)
        {

            Contact contact = db.Contacts.Find(id);
            int status = (contact.Status == 1) ? 0 : 1;
            contact.Status = status;
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Contacts");
        }
    }
}
