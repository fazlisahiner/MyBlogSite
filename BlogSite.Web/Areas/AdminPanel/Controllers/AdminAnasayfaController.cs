using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class AdminAnasayfaController : Controller
    {
        // GET: AdminPanel/AdminAnasayfa
        public ActionResult AnasayfaIndex()
        {
            return View();
        }
    }
}