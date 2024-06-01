using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class AdminMakaleController : Controller
    {
        // GET: AdminPanel/Makale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminMakaleDetay(int? id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir
            //int userId = getArticle.UserId;
            //var getAllCategories = db.Categories.ToList();

            //var articleCategory = db.Categories.Where(c => c.CategoryId == getArticle.CategoryId).FirstOrDefault();
            //var writer = db.Users.Where(us=>us.UsersId== userId).FirstOrDefault();
            var comment = db.Comments.Where(cmm => cmm.ArticleId == getArticle.ArticleId).ToList();
            var commentResponse = db.CommentResponse.Where(r => r.ArticleId == getArticle.ArticleId).ToList();
            var getAllUsers = db.Users.ToList();

            if (getArticle != null)
            {
                return View(Tuple.Create(getArticle, comment, getAllUsers, commentResponse));

            }
            return View();
        }
    }
}