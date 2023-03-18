using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CtrlS.Models;

namespace CtrlS.Controllers
{
    public class BlogEventsController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();

        // GET: BlogEvents
        public ActionResult Index()
        {
            return View(db.BlogEvents.ToList());
        }

        // GET: BlogEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEvent blogEvent = db.BlogEvents.Find(id);
            if (blogEvent == null)
            {
                return HttpNotFound();
            }
            return View(blogEvent);
        }

        // GET: BlogEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Cover,Content,Content2,Content3,Content4,Type,Img,ImgName,DateTime,DateTime2,Status")] BlogEvent blogEvent)
        {
            if (ModelState.IsValid)
            {
                db.BlogEvents.Add(blogEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogEvent);
        }

        // GET: BlogEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEvent blogEvent = db.BlogEvents.Find(id);
            if (blogEvent == null)
            {
                return HttpNotFound();
            }
            return View(blogEvent);
        }

        // POST: BlogEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Cover,Content,Content2,Content3,Content4,Type,Img,ImgName,DateTime,DateTime2,Status")] BlogEvent blogEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogEvent);
        }

        // GET: BlogEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogEvent blogEvent = db.BlogEvents.Find(id);
            if (blogEvent == null)
            {
                return HttpNotFound();
            }
            return View(blogEvent);
        }

        // POST: BlogEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogEvent blogEvent = db.BlogEvents.Find(id);
            db.BlogEvents.Remove(blogEvent);
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
