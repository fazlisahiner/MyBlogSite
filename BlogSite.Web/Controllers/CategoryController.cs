using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult CategoriesIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();
            var categories = db.Categories.ToList();
            return View(categories);
        }
    }
}