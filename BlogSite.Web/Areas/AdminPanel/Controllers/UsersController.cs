using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class UsersController : Controller
    {
        // GET: AdminPanel/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}