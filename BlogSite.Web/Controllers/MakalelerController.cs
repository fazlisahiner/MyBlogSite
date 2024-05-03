using BlogSite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class MakalelerController : Controller
    {


        //MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        // GET: Makaleler
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakaleDetayIndex(int? id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir
            //int userId = getArticle.UserId;
            var getAllCategories = db.Categories.ToList();
            //var articleCategory = db.Categories.Where(c => c.CategoryId == getArticle.CategoryId).FirstOrDefault();
            //var writer = db.Users.Where(us=>us.UsersId== userId).FirstOrDefault();
            var comment = db.Comments.Where(cmm => cmm.ArticleId == getArticle.ArticleId).ToList();
            var getAllUsers = db.Users.ToList();

            if (getArticle != null)
            {
                return View(Tuple.Create(getAllCategories, getArticle, comment, getAllUsers));

            }
            return View();
        }


        public JsonResult YorumEkle(int articleid, string yorumMetin)
        {
            int userId = 1;
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            //var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir

            //var getAllCategories = db.Categories.ToList();

            //var comment = db.Comments.Where(cmm => cmm.ArticleId == getArticle.ArticleId).ToList();
            //var getAllUsers = db.Users.ToList();
            //var kullanici = db.Users.Where(usr => usr.EMail == email);
            //var userId = kullanici.UsersId;



            if (userId == -1)
            {
                ViewBag.Message = "Yorum ekleyebilmek için üye olmanız gerekmektedir.";
                return Json(new { success = true });
            }
            else
            {


                Comments model = new Comments();
                model.ArticleId = articleid;
                model.UserId = userId;
                model.Content = yorumMetin;
                model.CreateDate = DateTime.Today;
                model.UpdateDate = DateTime.Today;
                db.Comments.Add(model);
                int result = db.SaveChanges();

                if (result > 0)
                {

                    CommentDTO commentDTO = new CommentDTO();
                    var user = db.Users.Where(k => k.UsersId == userId).FirstOrDefault();
                    commentDTO.ArticleId = model.ArticleId;
                    commentDTO.CommentDatetime = model.CreateDate;
                    commentDTO.UserName = user.UserName;
                    ViewBag.Message = "Yorum eklendi";
                    return Json(commentDTO);
                }
                else
                {
                    return Json(new { success = false });
                }
            }
        }
    }
}