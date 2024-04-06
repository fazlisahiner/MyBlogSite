using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: AdminPanel/Categories
        public ActionResult CategoryIndex()
        {
            return View();
        }
    }
}