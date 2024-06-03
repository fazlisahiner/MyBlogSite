using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class RolesController : Controller
    {
        // GET: AdminPanel/Roles
        public ActionResult RolesIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var roleList = db.Roles.ToList();
            return View(roleList);


        }

        public ActionResult RolesAdd()
        {

            return View();

        }

        [HttpPost]
        public ActionResult RolesAdd(FormCollection fc)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();

            string roleName = fc["ad"].Trim();

            var getirRol = db.Roles.Where(k => k.RoleName == roleName).FirstOrDefault();


            if (getirRol != null)
            {
                ViewBag.Message = "Bu rol daha önce eklendi! ";
                Thread.Sleep(3000);
            }
            else
            {
                Roles model = new Roles();
                model.RoleName = fc["ad"].Trim();
                model.Description = fc["acikla"].Trim();
                model.Permissions = fc["yetki"].Trim();
                model.CreateDate = DateTime.Today;
                model.UpdateDate = DateTime.Today;

                db.Roles.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Yeni rol ekleme başarılı ";
                Thread.Sleep(3000);
            }


            return View();

        }

        public ActionResult RoleDetail(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getRole = db.Roles.Where(k => k.RoleId == id).FirstOrDefault();
            if (getRole == null)
            {
                return HttpNotFound();
            }
            return View(getRole);
        }

        public ActionResult RoleEdit(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getRole = db.Roles.Where(k => k.RoleId == id).FirstOrDefault();
            if (getRole == null)
            {
                return HttpNotFound();
            }

            return View(getRole);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEdit(Roles model)
        {
            if (ModelState.IsValid)
            {
                MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
                var getRole = db.Roles.Where(k => k.RoleId == model.RoleId).FirstOrDefault();
                if (getRole == null)
                {
                    return HttpNotFound();
                }
                getRole.Permissions = model.Permissions;
                getRole.RoleName = model.RoleName;
                getRole.Description = model.Description;
                getRole.UpdateDate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("RolesIndex");

            }

            return View(model);

        }

        public ActionResult RoleDelete(int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getRole = db.Roles.Where(k => k.RoleId == id).FirstOrDefault();
            if (getRole == null)
            {
                return HttpNotFound();
            }
            return View(getRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleDeleteConfirmed (int id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var getRole = db.Roles.Where(k => k.RoleId == id).FirstOrDefault();
            if (getRole == null)
            {
                return HttpNotFound();
            }

            db.Roles.Remove(getRole);
            db.SaveChanges();

            return RedirectToAction("RolesIndex");
        }


    }
}