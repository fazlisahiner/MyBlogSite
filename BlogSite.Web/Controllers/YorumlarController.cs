using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class YorumlarController : Controller
    {
        // GET: Yorumlar
        MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();

        public ActionResult Index()
        {
           
            return View();
            
        }

        public ActionResult YorumlarIndex(int? id = null)
        {
            var getComment = db.Comments.Where(k => k.ArticleId == id).ToList();
            var getAllCategories = db.Categories.ToList();

            if (getComment != null)
            {
                return View(Tuple.Create(getAllCategories, getComment));

            }
            else
            {
                ViewBag.Message = "Bu makaleye henüz yorum yapılmadı. ";
                return View(Tuple.Create(getAllCategories, getComment));
            }
            

        }


    }
}