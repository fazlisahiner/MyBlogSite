using System;
using System.Collections.Generic;
using System.Linq;
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


        public ActionResult GirisIndex(string kullaniciAdi,string sifre)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getirKullanici = db.Users.Where(k => k.UserName == kullaniciAdi && k.UserPassword == sifre).FirstOrDefault();
            if (getirKullanici!=null)
            {
                //giriş

            }
            else
            {
                //uyarı mesajı
            }
            //Login 
            return View();
        }

        public ActionResult KullaniciKayitIndex()
        {
            return View();
        }
    }
}