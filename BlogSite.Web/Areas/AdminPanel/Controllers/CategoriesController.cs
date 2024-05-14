using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: AdminPanel/Categories
        public ActionResult CategoryIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var categoryList = db.Categories.ToList();
            return View(categoryList);
        }



        public ActionResult CategoryAdd()
        {

            return View();

        }

        [HttpPost]
        public ActionResult CategoryAdd(FormCollection fc)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();

            string catName = fc["category"].Trim();

            var getirCategory = db.Categories.Where(k => k.CategoryName == catName).FirstOrDefault();


            if (getirCategory != null)
            {
                ViewBag.Message = "Bu kategori daha önce eklendi! ";
                Thread.Sleep(3000);
            }
            else
            {
                if (catName == "")
                {
                    ViewBag.Message = "Kategori adı boş bırakılamaz";
                }
                else
                {
                    Categories model = new Categories();
                    model.CategoryName = fc["category"].Trim();

                    db.Categories.Add(model);
                    db.SaveChanges();
                    ViewBag.Message = "Yeni kategori ekleme başarılı ";
                    Thread.Sleep(3000);

                }

            }
            return View();

        }

        public ActionResult CategoryDetail(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getCategory = db.Categories.Where(k => k.CategoryId == id).FirstOrDefault();
            return View(getCategory);
        }

        public ActionResult CategoryEdit(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getCategory = db.Categories.Where(k => k.CategoryId == id).FirstOrDefault();

            return View(getCategory);

        }

        public ActionResult CategoryDelete(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getCategory = db.Categories.Where(k => k.CategoryId == id).FirstOrDefault();
            return View(getCategory);
        }




    }
}