using CtrlS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {

            if (ModelState.IsValid)
            {
                using (var context = new CtrlSDbContext())
                {
                    User users = context.Users
                                       .Where(u => u.UserName == UserName && u.Password == Password)
                                       .FirstOrDefault();
                    if (users != null)
                    {
                        Session["Name"] = users.Name;
                        Session["UserName"] = users.UserName;
                        Session["UserId"] = users.UserId;
                        if (users.UserName == "Admin")
                        {
                            return RedirectToAction("Index", "Contacts");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Contacts");
                        }
                    }
                    else
                    {
                        TempData["Loginerror"] = "<script>alert('Nhập lại tài khoản, mật khẩu đi pé ơiiii');</script>";
                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["Name"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            return RedirectToAction("Index", "Admin");
        }
    }
}