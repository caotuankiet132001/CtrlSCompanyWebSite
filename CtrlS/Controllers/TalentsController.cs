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
    public class TalentsController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();

        // GET: Talents
        [CustomAuthorize("Admin")]
        public ActionResult Status(int id)
        {

            Talent talent = db.Talents.Find(id);
            int status = (talent.Status == 1) ? 0 : 1;
            talent.Status = status;
            db.Entry(talent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Talents");
        }
        [CustomAuthorize("Admin")]

        public ActionResult Index()
        {
            return View(db.Talents.ToList());
        }
        [CustomAuthorize("Admin")]

        // GET: Talents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talent talent = db.Talents.Find(id);
            if (talent == null)
            {
                return HttpNotFound();
            }
            return View(talent);
        }
        [CustomAuthorize("Admin")]

        // GET: Talents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Talents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuthorize("Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string Role, HttpPostedFileBase file)
        {
            Talent talents = new Talent();
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
            talents.Status = 0;
            talents.Role = Role;
            talents.Name = Name;
            talents.Img = file.ToString();
            var fileName = Path.GetFileName(file.FileName);
            var ext = Path.GetExtension(file.FileName);
            if (allowedExtensions.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = name + "_" + talents.Id + ext;
                var path = Path.Combine(Server.MapPath("~/Img/Talents/"), myfile);
                talents.Img = path;
                talents.ImgName = fileName.ToString();
                talents.DateTime = DateTime.Now;
                talents.DateTime2 = DateTime.Now;
                db.Talents.Add(talents);
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

        // GET: Talents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talent talent = db.Talents.Find(id);
            if (talent == null)
            {
                return HttpNotFound();
            }
            return View(talent);
        }
        [CustomAuthorize("Admin")]

        // POST: Talents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Role,Img,ImgName,DateTime,DateTime2,Status")] Talent talent, HttpPostedFileBase file)
        {
            if (file != null) //có cập nhật hình hay ko
            {
                talent.Img = "";
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                talent.Img = file.ToString(); //getting complete url  
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + "_" + talent.Id + ext; //appending the name with id                                                           
                    var path = Path.Combine(Server.MapPath("~/Img"), myfile); // store the file inside ~/project folder(Img)  
                    talent.Img = path;
                    talent.ImgName = fileName.ToString();
                    talent.DateTime2 = DateTime.Now;
                    db.Entry(talent).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                }
                return View(talent);
            }
            else
            {
                talent.DateTime2 = DateTime.Now;
                db.Entry(talent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [CustomAuthorize("Admin")]

        // GET: Talents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talent talent = db.Talents.Find(id);
            if (talent == null)
            {
                return HttpNotFound();
            }
            return View(talent);
        }
        [CustomAuthorize("Admin")]

        // POST: Talents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Talent talent = db.Talents.Find(id);
            db.Talents.Remove(talent);
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
