using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        //public ActionResult Index()
        //{
        //    return View();   bunu kullanmıyacaz
        //}

        

        public ActionResult AnasayfaIndex(int? id=null) // burdaki soru işareti eğer parametre gelmezsenull kabul et demek için
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();


            if (id==null)
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