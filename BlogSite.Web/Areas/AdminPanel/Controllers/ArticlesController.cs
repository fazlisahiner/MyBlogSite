using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: AdminPanel/Articles
        public ActionResult ArticlesIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var makaleList = db.Articles.ToList();
            return View(makaleList);
        }


        public ActionResult ArticlesAdd()
        {

            return View();

        }
    }
}