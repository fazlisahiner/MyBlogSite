using BlogSite.Web;
using BlogSite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BlogSite.Web.Areas.AdminPanel.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class ArticlesController : Controller
    {
        MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        // GET: AdminPanel/Articles
        public ActionResult ArticlesIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Users = db.Users.ToList();
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
                //string gelenMakale = HttpUtility.HtmlEncode(form["icerik"]);

                string gelenMakale = form["icerik"];


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
                        //model.Content = System.Web.HttpUtility.HtmlDecode(gelenMakale);
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

        //public ActionResult ArticleDetail(int id)
        //{
        //    MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        //    var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();
        //    //Listeler model = new Listeler();
        //    //model.Userslar = db.Users.ToList();
        //    //model.Categoriesler = db.Categories.ToList();
        //    //model.Makale = getArticle;

        //    //ViewBag.Categories = db.Categories.ToList();
        //    //ViewBag.Users = db.Users.ToList();
            
        //    //ViewBag.User = db.Users.Where(u => u.UsersId == getArticle.UserId);
        //    return View(getArticle);
        //}

        // ArticleEdit metodu kullanılmıyor, onun yerine MakaleGuncelle metodu kullanılıyor.
        public ActionResult ArticleEdit (int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();
            
            return View(getArticle);

        }
        // ArticleEdit metodu kullanılmıyor, onun yerine MakaleGuncelle metodu kullanılıyor.

        // GET: ArticleDelete
        public ActionResult ArticleDelete ( int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();

            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();

            if (getArticle == null)
            {
                return HttpNotFound();
            }
            return View(getArticle);
        }

        // POST: ArticleDelete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleDeleteConfirmed(int id)
        {
            var getArticle = db.Articles.FirstOrDefault(k => k.ArticleId == id);
            if (getArticle == null)
            {
                return HttpNotFound();
            }

            db.Articles.Remove(getArticle);
            db.SaveChanges();

            return RedirectToAction("ArticlesIndex");
        }



        public ActionResult MakaleDetayi(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();
            var getUser = db.Users.Where(us => us.UsersId == getArticle.UserId).FirstOrDefault();
            var getCategory = db.Categories.Where(cat => cat.CategoryId == getArticle.CategoryId).FirstOrDefault();

            MakaleDetay model = new MakaleDetay();

            model.Article = getArticle;
            model.User = getUser;
            model.Category = getCategory;

            return View(model);
        }

        public ActionResult MakaleGuncelle(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();


            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();

            if (getArticle == null) {
                return HttpNotFound();
            }
            else {
            //var getUser = db.Users.Where(us => us.UsersId == getArticle.UserId).FirstOrDefault();
            //var getCategory = db.Categories.Where(cat => cat.CategoryId == getArticle.CategoryId).FirstOrDefault();

            MakaleDetay model = new MakaleDetay();

            model.Article = getArticle;
            //model.User = getUser;
            //model.Category = getCategory;
            //model.Users = db.Users.ToList();
            //model.Categories = db.Categories.ToList();

            return View(model);
            }
        }
        
        [ValidateInput(false),  HttpPost]
        public ActionResult MakaleGuncelle(MakaleDetay model)
        {
            

            if (ModelState.IsValid)
            {
                using (MyBlogSiteDBEntities db = new MyBlogSiteDBEntities())
                {
                    var makale = db.Articles.Where(k => k.ArticleId == model.Article.ArticleId).FirstOrDefault();
                    if (makale == null)
                    {
                        return HttpNotFound();
                    }

                    makale.Title = model.Article.Title;
                  


                    //makale.Content = HttpUtility.HtmlEncode(model.Article.Content);

                    makale.Content =  model.Article.Content;

                   
                    makale.UpdateDate = DateTime.Now;
                    

                    db.Entry(makale).State = EntityState.Modified;
                    db.SaveChanges();

                   

                    return RedirectToAction("ArticlesIndex");
                }
            }
            else
            {
                TempData["SummernoteIcerik"] = model.Article.Content;
                return View(model);
            }

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