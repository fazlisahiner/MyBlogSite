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
            var commentResponse = db.CommentResponse.Where(r => r.ArticleId == getArticle.ArticleId).ToList();
            var getAllUsers = db.Users.ToList();

            if (getArticle != null)
            {
                return View(Tuple.Create( getArticle, comment, getAllUsers, commentResponse));

            }
            return View();
        }

        //public JsonResult YorumEkle(int articleid, string yorumMetin, string email)
        //{
        //    return YorumEkle(articleid, yorumMetin, email);
        //}


        public JsonResult YorumEkle(int articleid, string yorumMetin, string email)
        {
            
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            //var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir

            //var getAllCategories = db.Categories.ToList();

           
            var kullanici = db.Users.Where(usr => usr.EMail == email).FirstOrDefault();
            int usId = kullanici.UsersId;

            


            if (kullanici == null)
            {
                //CommentDTO commentDTO = new CommentDTO();
                //commentDTO.UserName= null;
                //ViewBag.Message = "Yorum ekleyebilmek için üye olmanız gerekmektedir.";
               
                return Json(new { success = false });

            }
            else
            {

                
                Comments model = new Comments();
                model.ArticleId = articleid;
                model.UserId = usId;
                model.Content =  yorumMetin;
                model.CreateDate = DateTime.Today;
                model.UpdateDate = DateTime.Today;
                db.Comments.Add(model);
                int result = db.SaveChanges();

                if (result > 0)
                {

                    CommentDTO commentDTO = new CommentDTO();
                    //var user = db.Users.Where(k => k.UsersId == usId).FirstOrDefault();
                    commentDTO.UserId = usId;
                    commentDTO.ArticleId = model.ArticleId;
                    commentDTO.CommentDatetime = model.CreateDate;
                    commentDTO.UserName = kullanici.UserName;
                    commentDTO.CommentContent = model.Content;
                    //ViewBag.Message = "Yorum eklendi";
                    return Json(commentDTO);
                }
                else
                {
                    return Json(new { success = false });
                }
            }
        }


        public JsonResult YorumCevapla(int yorumId, string cevapMetni, string cevapVerenMail, int articleId)
        {

            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            //var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir

            //var getAllCategories = db.Categories.ToList();


            var kullanici = db.Users.Where(usr => usr.EMail == cevapVerenMail).FirstOrDefault();
            int usId = kullanici.UsersId;




            if (kullanici == null)
            {
                //CommentDTO commentDTO = new CommentDTO();
                //commentDTO.UserName= null;
                //ViewBag.Message = "Yorum ekleyebilmek için üye olmanız gerekmektedir.";

                return Json(new { success = false });

            }
            else
            {


                CommentResponse model = new CommentResponse();
                model.CommentId = yorumId;
                model.UserId = usId;
                model.ResponseContent = cevapMetni;
                model.CreateDate = DateTime.Today;
                model.ArticleId = articleId;


                db.CommentResponse.Add(model);
                int result = db.SaveChanges();

                if (result > 0)
                {

                    CommentResponseDTO commentResponseDTO = new CommentResponseDTO();
                    //var user = db.Users.Where(k => k.UsersId == usId).FirstOrDefault();
                    commentResponseDTO.UserId = usId;
                    commentResponseDTO.CommentID = model.CommentId;
                    commentResponseDTO.ResponseDate = model.CreateDate;
                    commentResponseDTO.UserName = kullanici.UserName;
                    commentResponseDTO.YorumCevapMetni = model.ResponseContent;
                    //ViewBag.Message = "Yorum eklendi";
                    return Json(commentResponseDTO);
                }
                else
                {
                    return Json(new { success = false });
                }
            }
        }
    }
}