using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class HakkimdaController : Controller
    {
        // GET: Hakkimda
        public ActionResult HakkimdaIndex(int? id = null)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();


            if (id == null)
            {
                var getAllArticles = db.Articles.ToList();
                var getAllCategories = db.Categories.ToList();

                return View(Tuple.Create(getAllCategories, getAllArticles));
                //return View(getAllArticles);
            }
            else
            {
                var getArticleWithCategory = db.Articles.Where(g => g.CategoryId == id).ToList();
                var getAllCategories = db.Categories.ToList();

                return View(Tuple.Create(getAllCategories, getArticleWithCategory));
                // return View(getArticleWithCategory);
            }
           
        }
    }
}