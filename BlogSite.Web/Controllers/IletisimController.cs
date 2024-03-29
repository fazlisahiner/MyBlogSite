using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Controllers
{
    public class IletisimController : Controller
    {
        // GET: Iletisim
        public ActionResult IletisimIndex()
        {
            return View();
        }
    }
}