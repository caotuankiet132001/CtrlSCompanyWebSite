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
    public class EventsController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();
        [CustomAuthorize("Admin")]
        public ActionResult Status(int id)
        {
            Event @event = db.Events.Find(id);
            int status = (@event.Status == 1) ? 0 : 1;
            @event.Status = status;
            db.Entry(@event).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Events");
        }

        [CustomAuthorize("Admin")]
        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }
        [CustomAuthorize("Admin")]
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }
        [CustomAuthorize("Admin")]
        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Content, HttpPostedFileBase file)
        {
            string _FileName = Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/Content/Image/Events/"), _FileName);
            file.SaveAs(_path);
            Event events = new Event();
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
            events.Status = 0;
            events.Content = Content;
            events.Img = file.ToString();
            var fileName = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = name + "_" + events.Id + ext;
                var path = Path.Combine(Server.MapPath("~/Content/Image/Events/"), myfile);
                events.Img = path;
                events.ImgName = fileName.ToString();
                events.DateTime = DateTime.Now;
                events.DateTime2 = DateTime.Now;
                events.file = Path.Combine(Server.MapPath("~/Content/Image/Events/"), Path.GetFileName(file.FileName));
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "<script>alert('Chỉ chấp nhận các định dạng sau: png, jpg, jpeg, Jpg');</script>";
            }
            return View();
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Content,Img,ImgName,DateTime,DateTime2,Status,file")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Events.Add(@event);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(@event);
        //}

        // GET: Events/Edit/5
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,Img,ImgName,DateTime,DateTime2,Status,file")] Event @event, HttpPostedFileBase file)
        {
            if (file != null) //có cập nhật hình hay ko
            {
                @event.Img = "";
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                @event.Img = file.ToString(); //getting complete url  
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + "_" + @event.Id + ext; //appending the name with id                                                           
                    var path = Path.Combine(Server.MapPath("~/Content/Image/Events/"), myfile); // store the file inside ~/project folder(Img)  
                    @event.Img = path;
                    @event.ImgName = fileName.ToString();
                    @event.DateTime2 = DateTime.Now;
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                }
                return View(@event);
            }
            else
            {
                @event.DateTime2 = DateTime.Now;
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            //if (ModelState.IsValid)
            //{
            //    db.Entry(@event).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [CustomAuthorize("Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
