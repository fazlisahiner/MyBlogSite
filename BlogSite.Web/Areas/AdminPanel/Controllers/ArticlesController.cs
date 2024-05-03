using BlogSite.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var categoryList = db.Categories.ToList();

            //var userList = db.Users.ToList();

            return View(categoryList);
            

        }

        [ValidateInput(false), HttpPost]
        public ActionResult ArticlesAdd(FormCollection form)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var categoryList = db.Categories.ToList();
            
            string catName = form["category"].Trim();
            

            if (form["baslik"].Trim() == "")
            {
                ViewBag.Message = "Başlık alanı boş olamaz";
            }
            else
            {
                string gelenMakale = Server.HtmlEncode(form["icerik"]);
                



                if ( gelenMakale.Trim() == "" ) 
                {

                    ViewBag.Message = "İçerik alanı boş olamaz";
                }
                else
                {
                    if (catName == "Seçiniz")
                    {
                        ViewBag.Message = "Lütfen kategori seçiniz";
                    }
                    else
                    {
                        var category = db.Categories.Where(k => k.CategoryName == catName).FirstOrDefault();
                        int cat = category.CategoryId;
                        Articles model = new Articles();
                        model.Title = form["baslik"];
                        model.Content = gelenMakale;
                        model.CategoryId = cat;


                        model.UserId = 1; // bunu ayarlayacaz
                        model.CreateDate = DateTime.Today;
                        model.Views = 0;
                        model.Likes = 0;

                        db.Articles.Add(model);
                        db.SaveChanges();
                        ViewBag.Message = "Yeni makale ekleme başarılı ";
                        Thread.Sleep(3000);

                    }
                }

            }

          return View(categoryList);


        }
    }
}


//if (form["baslik"].Trim() == "")
//{
//    ViewBag.Message = "Başlık alanı boş olamaz";
//}
//else
//{
//    if (form["icerik"].Trim() == null)
//    {

//        ViewBag.Message = "İçerik alanı boş olamaz";
//    }
//    else
//    {
//        if (catName == "Seçiniz")
//        {
//            ViewBag.Message = "Lütfen kategori seçiniz";
//        }
//        else
//        {
//            var category = db.Categories.Where(k => k.CategoryName == catName).FirstOrDefault();
//            int cat = category.CategoryId;
//            Articles model = new Articles();
//            model.Title = form["baslik"];
//            model.Content = form["icerik"];
//            model.CategoryId = cat;


//            model.UserId = 1; // bunu ayarlayacaz
//            model.CreateDate = DateTime.Today;
//            model.Views = 0;
//            model.Likes = 0;

//            db.Articles.Add(model);
//            db.SaveChanges();
//            ViewBag.Message = "Yeni makale ekleme başarılı ";
//            Thread.Sleep(3000);

//        }
//    }

//}