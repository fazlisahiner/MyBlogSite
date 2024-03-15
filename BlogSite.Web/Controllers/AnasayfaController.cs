using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        //public ActionResult Index()
        //{
        //    return View();   bunu kullanmıyacaz
        //}

        //https://www.free-css.com/free-css-templates/page272/mundana
        public ActionResult AnasayfaIndex()
        {
            return View();
        }
    }
}