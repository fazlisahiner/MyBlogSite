using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class CommentsController : Controller
    {
        // GET: AdminPanel/Comments
        public ActionResult CommentsIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var commentList = db.Comments.ToList();

            return View(commentList);
        }

        public ActionResult CommentsAdd()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var articleList = db.Articles.ToList();

            return View(articleList);
        }

        [HttpPost]
        public ActionResult CommentsAdd(FormCollection form)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var articleList = db.Articles.ToList();

            if (form["yorum"].Trim() == "")
            {
                ViewBag.Message = "Yorum alanı boş olamaz";
            }
            else
            {
                if (form["article"] == "Seçiniz")
                {
                    ViewBag.Message = "Lütfen yorum yapacağınız makaleyi seçiniz";
                }
                else
                {
                    string article = form["article"].Trim();

                    var title = db.Articles.Where(k => k.Title == article).FirstOrDefault();
                    int id = title.ArticleId;
                    Comments model = new Comments();

                    model.ArticleId = id;
                    model.Content = form["yorum"];
                    model.CreateDate = DateTime.Today;
                    model.UpdateDate= DateTime.Today;
                    model.UserId = 0; // bunu ayarlayacaz

                    db.Comments.Add(model);
                    db.SaveChanges();
                    ViewBag.Message = "Yorum ekleme başarılı ";

                }

            }


            return View(articleList);
        }

        public ActionResult CommentDetail(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getComment = db.Comments.Where(k => k.CommetId == id).FirstOrDefault();
            var user = db.Users.Where(us => us.UsersId == getComment.UserId).FirstOrDefault();
            var article = db.Articles.Where(ar => ar.ArticleId == getComment.ArticleId).FirstOrDefault();
            return View(Tuple.Create(getComment, user, article) );
        }

        public ActionResult CommentEdit(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getComment = db.Comments.Where(k => k.CommetId == id).FirstOrDefault();

            var user = db.Users.Where(us => us.UsersId == getComment.UserId).FirstOrDefault();
            var article = db.Articles.Where(ar => ar.ArticleId == getComment.ArticleId).FirstOrDefault();

            return View(getComment);

        }

        public ActionResult CommentDelete(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getComment = db.Comments.Where(k => k.CommetId == id).FirstOrDefault();
            //db.Comments.Remove(getComment);
            return View(getComment);
        }

    }
}