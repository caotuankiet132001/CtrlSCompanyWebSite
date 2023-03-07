using CtrlS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlS.Controllers
{
    [CustomAuthenticationFilter]
    public class AdminController : Controller
    {
        // GET: Admin
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize("Admin")]
        public ActionResult Blog()
        {
            return View();
        }
        [CustomAuthorize("Admin")]
        public ActionResult SuaBlog()
        {
            return View();
        }
    }
}