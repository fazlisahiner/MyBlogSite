using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class MakalelerController : Controller
    {


        MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        // GET: Makaleler
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakaleDetayIndex(int? id)
        {
            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir
            var getAllCategories = db.Categories.ToList();

            if (getArticle!=null)
            {
                return View(Tuple.Create(getAllCategories, getArticle));

            }
            return View();
        }



    }
}