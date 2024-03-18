using System;
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
            return View();
        }
    }
}