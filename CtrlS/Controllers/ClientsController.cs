using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CtrlS.Helper;
using CtrlS.Models;

namespace CtrlS.Controllers
{
    public class ClientsController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();
        [CustomAuthorize("Admin")]

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }
        [CustomAuthorize("Admin")]

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [CustomAuthorize("Admin")]

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, HttpPostedFileBase file)
        {
            Client clients = new Client();
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
            clients.Status = 0;
            clients.Name = Name;
            clients.Img = file.ToString();
            var fileName = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = name + "_" + clients.Id + ext;
                var path = Path.Combine(Server.MapPath("~/Img/Clients/"), myfile);
                clients.Img = path;
                clients.ImgName = fileName.ToString();
                clients.DateTime = DateTime.Now;
                clients.DateTime2 = DateTime.Now;
                db.Clients.Add(clients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "<script>alert('Chỉ chấp nhận các định dạng sau: png, jpg, jpeg, Jpg');</script>";
            }
            return View();
        }
        [CustomAuthorize("Admin")]

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [CustomAuthorize("Admin")]

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Img,ImgName,DateTime,DateTime2,Status")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }
        [CustomAuthorize("Admin")]

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [CustomAuthorize("Admin")]

        public ActionResult Status(int id)
        {

            Client client = db.Clients.Find(id);
            int status = (client.Status == 1) ? 0 : 1;
            client.Status = status;
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Clients");
        }
        [CustomAuthorize("Admin")]

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
    }
}
