﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class HakkimdaController : Controller
    {
        // GET: Hakkimda
        public ActionResult HakkimdaIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();

            var getAllArticles = db.Articles.ToList();
            var getAllCategories = db.Categories.ToList();

            return View(Tuple.Create(getAllCategories, getAllArticles));

           
           
        }
    }
}