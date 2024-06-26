﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult Index()
        {
            return View();
        }

        MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();

        //public ActionResult GirisIndex(string kullaniciAdi,string sifre)
        //{
        //    MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        //    var getirKullanici = db.Users.Where(k => k.UserName == kullaniciAdi && k.UserPassword == sifre).FirstOrDefault();
        //    if (getirKullanici!=null)
        //    {
        //        //giriş=> Anasayfaya git
        //        Session["Yetki"] = kullaniciAdi;
        //        return Redirect("/Anasayfa/AnasayfaIndex");
        //    }
        //    else
        //    {
        //        //uyarı mesajı
        //        Console.WriteLine("Kullanıcı bulunamadı! ");
        //        Thread.Sleep(3000);
        //        return View();
        //    }
        //    //Login 

        //}

        public ActionResult GirisIndex()
        {
            
                return View();
            

        }

        [HttpPost]
        public ActionResult GirisIndex(FormCollection fc)
        {
            string email = fc["email"];
            string sifre = fc["sifre"];

            //MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getirKullanici = db.Users.Where(k => k.EMail == email && k.UserPassword == sifre).FirstOrDefault();
            if (getirKullanici != null)
            {
                //giriş=> Anasayfaya git
                Session["Yetki"] = email;
                return Redirect("/Anasayfa/AnasayfaIndex");
            }
            else
            {
                //uyarı mesajı
               ViewBag.Message = "Kullanıcı adı veya şifre hatalı! ";
                
                Thread.Sleep(3000);
                return View();
            }
            //Login 

        }



        public ActionResult KullaniciKayitIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KullaniciKayitIndex(FormCollection form)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
                // bunu yazıp alt+shift+f10

            Users model = new Users();
            model.UserName = form["isimInput"].Trim();
            model.EMail= form["emailInput"].Trim() ;
            model.UserPassword= form["sifreInput"].Trim();

            db.Users.Add(model);
            db.SaveChanges();

            return View();
        }


    }

    
}