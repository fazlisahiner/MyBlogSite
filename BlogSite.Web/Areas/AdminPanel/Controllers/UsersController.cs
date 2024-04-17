using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Web.Areas.AdminPanel.Controllers
{
    public class UsersController : Controller
    {
        // GET: AdminPanel/Users
        public ActionResult UsersIndex()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var userList = db.Users.ToList();
            return View(userList);
        }


        public ActionResult UserAdd()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var role = db.Roles.ToList();
            return View(role);

        }

        [HttpPost]
        public ActionResult UserAdd(FormCollection form)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            var role = db.Roles.ToList();

            string roleName = form["role"].Trim();


            if (form["kulAdi"].Trim() == "")
            {
                ViewBag.Message = "Kullanıcı adı alanı boş olamaz";
            }
            else
            {
                if (form["email"].Trim() == "")
                {

                    ViewBag.Message = "Email alanı boş olamaz";
                }
                else
                {
                    string email = form["email"].Trim();

                    var getirEmail = db.Users.Where(k => k.EMail == email).FirstOrDefault();


                    if (getirEmail != null)
                    {
                        ViewBag.Message = "Bu e-mail daha önce kullanıldı, lütfen farklı bir e-mail kullanın! ";
                        Thread.Sleep(3000);
                    }
                    else
                    {

                        if (form["sifre"].Trim() == "")
                        {

                            ViewBag.Message = "Şifre alanı boş olamaz";
                        }
                        else
                        {
                            if (roleName == "Seçiniz")
                            {
                                ViewBag.Message = "Lütfen rol seçiniz";
                            }
                            else
                            {

                                var rol = db.Roles.Where(k => k.RoleName == roleName).FirstOrDefault();
                                int roleId = rol.RoleId;
                                Users model = new Users();
                                model.UserName = form["kulAdi"].Trim();
                                model.EMail = form["email"];
                                model.RoleId = roleId;
                                model.UserPassword = form["sifre"];
                                model.IsActive = true;
                                model.CreateDate = DateTime.Now;



                                db.Users.Add(model);
                                db.SaveChanges();
                                ViewBag.Message = "Yeni kullanıcı ekleme başarılı ";
                                Thread.Sleep(3000);

                            }
                        }
                    }
                }
            }


            return View(role);

        }
    }
}