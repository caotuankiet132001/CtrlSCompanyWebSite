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
    
    public class BlogsController : Controller
    {
        private CtrlSDbContext db = new CtrlSDbContext();

        // GET: Blogs
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            return View(db.Blogs.OrderByDescending(m=>m.DateTime2).ToList());
        }
        [CustomAuthorize("Admin")]
        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        // GET: Blogs/Create
        [CustomAuthorize("Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult Create(string Title, string Content, HttpPostedFileBase file)
        {
            Blog blogs = new Blog();
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
            blogs.Status = 0;
            blogs.Title = Title;
            blogs.Content = Content;
            blogs.Img = file.ToString(); 
            var fileName = Path.GetFileName(file.FileName); 
            var ext = Path.GetExtension(file.FileName); 
            if (allowedExtensions.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(fileName); 
                string myfile = name + "_" + blogs.Id + ext;                                                      
                var path = Path.Combine(Server.MapPath("~/Img"), myfile); 
                blogs.Img = path;
                blogs.ImgName = fileName.ToString();
                blogs.DateTime = DateTime.Now;
                blogs.DateTime2 = DateTime.Now;
                db.Blogs.Add(blogs);
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
        public ActionResult Status(int id)
        {

            Blog blog = db.Blogs.Find(id);
            int status = (blog.Status == 1) ? 0 : 1;
            blog.Status = status;
            db.Entry(blog).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Blogs");
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Bind(Include = "Id,Category,Title,Content,Img,DateTime")] Blog blog
        //public ActionResult Create(HttpPostedFileBase [] Img, string Title, string Content)
        //{
        //    foreach(HttpPostedFileBase img in Img)
        //    {
        //        if (img != null || img.ContentLength > 0)
        //        {
        //            string _img = Path.GetFileName(img.FileName);
        //            string path = Path.Combine(Server.MapPath("/Img/"), _img);
        //            if (System.IO.File.Exists(path))
        //            {
        //                System.IO.File.Delete(path);
        //                img.SaveAs(path);
        //            }
        //            else
        //            {
        //                img.SaveAs(path);
        //            }

        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Blog blog = new Blog();
        //        blog.Title = Title;
        //        blog.Content = Content;
        //        blog.Img = Img;
        //        blog.DateTime = DateTime.Now;
        //        db.Blogs.Add(blog);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        // GET: Blogs/Edit/5
        [CustomAuthorize("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Img,ImgName,DateTime,DateTime2")] Blog blog, HttpPostedFileBase file)
        {
            if (file != null) //có cập nhật hình hay ko
            {
                blog.Img = "";
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                blog.Img = file.ToString(); //getting complete url  
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + "_" + blog.Id + ext; //appending the name with id                                                           
                    var path = Path.Combine(Server.MapPath("~/Img"), myfile); // store the file inside ~/project folder(Img)  
                    blog.Img = path;
                    blog.ImgName = fileName.ToString();
                    blog.DateTime2 = DateTime.Now;
                    db.Entry(blog).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                }
                return View(blog);
            }
            else
            {
                blog.DateTime2 = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            


            //if (ModelState.IsValid)
            //{
            //    db.Entry(blog).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(blog);
        }

        // GET: Blogs/Delete/5
        [CustomAuthorize("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
