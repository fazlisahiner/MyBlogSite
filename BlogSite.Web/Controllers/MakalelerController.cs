using BlogSite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading.Tasks;

namespace BlogSite.Web.Controllers
{
    public class MakalelerController : Controller
    {


        //MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        // GET: Makaleler
        public ActionResult Index()
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();
            return View();
        }

        public ActionResult MakaleDetayIndex(int? id)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();
            var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir
            //int userId = getArticle.UserId;
            var getAllCategories = db.Categories.ToList();
            
            //var articleCategory = db.Categories.Where(c => c.CategoryId == getArticle.CategoryId).FirstOrDefault();
            //var writer = db.Users.Where(us=>us.UsersId== userId).FirstOrDefault();
            var comment = db.Comments.Where(cmm => cmm.ArticleId == getArticle.ArticleId).ToList();
            var commentResponse = db.CommentResponse.Where(r => r.ArticleId == getArticle.ArticleId).ToList();
            var getAllUsers = db.Users.ToList();

            if (getArticle != null)
            {
                return View(Tuple.Create( getArticle, comment, getAllUsers, commentResponse));

            }
            return View();
        }

        //public JsonResult YorumEkle(int articleid, string yorumMetin, string email)
        //{
        //    return YorumEkle(articleid, yorumMetin, email);
        //}

        private readonly EmailValidator _emailValidator = new EmailValidator();

        public async Task<JsonResult> YorumEkle(int articleid, string yorumMetin, string email)
        {

            

            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();





            // Email doğrulama fonksiyonu
            bool IsValidEmail(string emailToValidate)
            {
                if (string.IsNullOrWhiteSpace(emailToValidate))
                    return false;

                try
                {
                    // Normalize the domain
                    emailToValidate = Regex.Replace(emailToValidate, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));

                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();

                        // Pull out and process domain name (throws ArgumentException on invalid)
                        string domainName = idn.GetAscii(match.Groups[2].Value);

                        return match.Groups[1].Value + domainName;
                    }
                }
                catch (RegexMatchTimeoutException e)
                {
                    return false;
                }
                catch (ArgumentException e)
                {
                    return false;
                }

                try
                {
                    return Regex.IsMatch(emailToValidate,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }


            //emailin gerçekte var olup olmadığını kontrol eden metod
            //bool isEmailValid = await _emailValidator.VerifyEmailAsync(email);

            //if (!isEmailValid)
            //{
            //    return Json(new { success = false, message = "Geçersiz e-posta adresi." });
            //}


            // Email maskalama fonksiyonu
            string MaskEmail(string emailToMask)
            {
                int indexOfAt = emailToMask.IndexOf('@');
                if (indexOfAt > 3)
                {
                    string maskedPart = new string('*', indexOfAt - 3);
                    return emailToMask.Substring(0, 3) + maskedPart + emailToMask.Substring(indexOfAt);
                }
                else
                {
                    return emailToMask; // Eğer email 3 harften kısa ise, maskelenmeden döner
                }
            }

            if (!IsValidEmail(email))
            {
                return Json(new { success = false, message = "Geçersiz email adresi." });
            }

            Comments model = new Comments();
            model.ArticleId = articleid;
            model.Content = yorumMetin;
            model.CreateDate = DateTime.Today;
            model.UpdateDate = DateTime.Today;
            model.UserName = MaskEmail(email);

            db.Comments.Add(model);
            int result = db.SaveChanges();

            if (result > 0)
            {
                CommentDTO commentDTO = new CommentDTO
                {
                    CommetId = model.CommetId,
                    ArticleId = model.ArticleId,
                    CommentDatetime = model.CreateDate,
                    UserName = MaskEmail(email), // Maskelenmiş email
                    CommentContent = model.Content
                };

                return Json(new { success = true, comment = commentDTO });
            }
            else
            {
                return Json(new { success = false });
            }




        }

        //private async Task<bool> VerifyEmailAsync(string email)
        //{
        //    throw new NotImplementedException();
        //}


        //public JsonResult YorumCevapla(int yorumId, string cevapMetni, string cevapVerenMail, int articleId)
        //{

        //    MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
        //    ViewBag.CategoryList = db.Categories.ToList();
        //    //var getArticle = db.Articles.Where(k => k.ArticleId == id).FirstOrDefault();//eşeleşen ilk değeri getir

        //    //var getAllCategories = db.Categories.ToList();


        //    var kullanici = db.Users.Where(usr => usr.EMail == cevapVerenMail).FirstOrDefault();
        //    int usId = kullanici.UsersId;




        //    if (kullanici == null)
        //    {
        //        //CommentDTO commentDTO = new CommentDTO();
        //        //commentDTO.UserName= null;
        //        //ViewBag.Message = "Yorum ekleyebilmek için üye olmanız gerekmektedir.";

        //        return Json(new { success = false });

        //    }
        //    else
        //    {


        //        CommentResponse model = new CommentResponse();
        //        model.CommentId = yorumId;
        //        model.UserId = usId;
        //        model.ResponseContent = cevapMetni;
        //        model.CreateDate = DateTime.Today;
        //        model.ArticleId = articleId;


        //        db.CommentResponse.Add(model);
        //        int result = db.SaveChanges();

        //        if (result > 0)
        //        {

        //            CommentResponseDTO commentResponseDTO = new CommentResponseDTO();
        //            //var user = db.Users.Where(k => k.UsersId == usId).FirstOrDefault();
        //            commentResponseDTO.UserId = usId;
        //            commentResponseDTO.CommentID = model.CommentId;
        //            commentResponseDTO.ResponseDate = model.CreateDate;
        //            commentResponseDTO.UserName = kullanici.UserName;
        //            commentResponseDTO.YorumCevapMetni = model.ResponseContent;
        //            //ViewBag.Message = "Yorum eklendi";
        //            return Json(commentResponseDTO);
        //        }
        //        else
        //        {
        //            return Json(new { success = false });
        //        }
        //    }
        //}     BU KODLARIN YERİNE ALTTAKİLERİ YAZDIM

        public JsonResult YorumCevapla(int yorumId, string cevapMetni, string cevapVerenMail, int articleId)
        {
            MyBlogSiteDBEntities db = new MyBlogSiteDBEntities();
            ViewBag.CategoryList = db.Categories.ToList();

            // Email doğrulama fonksiyonu
            bool IsValidEmail(string emailToValidate)
            {
                if (string.IsNullOrWhiteSpace(emailToValidate))
                    return false;

                try
                {
                    // Normalize the domain
                    emailToValidate = Regex.Replace(emailToValidate, @"(@)(.+)$", DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));

                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();

                        // Pull out and process domain name (throws ArgumentException on invalid)
                        string domainName = idn.GetAscii(match.Groups[2].Value);

                        return match.Groups[1].Value + domainName;
                    }
                }
                catch (RegexMatchTimeoutException e)
                {
                    return false;
                }
                catch (ArgumentException e)
                {
                    return false;
                }

                try
                {
                    return Regex.IsMatch(emailToValidate,
                        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }

            // Email maskalama fonksiyonu
            string MaskEmail(string emailToMask)
            {
                int indexOfAt = emailToMask.IndexOf('@');
                if (indexOfAt > 3)
                {
                    string maskedPart = new string('*', indexOfAt - 3);
                    return emailToMask.Substring(0, 3) + maskedPart + emailToMask.Substring(indexOfAt);
                }
                else
                {
                    return emailToMask; // Eğer email 3 harften kısa ise, maskelenmeden döner
                }
            }

            if (!IsValidEmail(cevapVerenMail))
            {
                return Json(new { success = false, message = "Geçersiz email adresi." });
            }

            //var kullanici = db.Users.Where(usr => usr.EMail == cevapVerenMail).FirstOrDefault();
            //if (kullanici == null)
            //{
            //    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            //}

            //int usId = kullanici.UsersId;

            CommentResponse model = new CommentResponse();
            model.CommentId = yorumId;
            //model.UserId = usId;
            model.ResponseContent = cevapMetni;
            model.CreateDate = DateTime.Today;
            model.ArticleId = articleId;
            model.UserName = MaskEmail(cevapVerenMail);

            db.CommentResponse.Add(model);
            int result = db.SaveChanges();

            if (result > 0)
            {
                CommentResponseDTO commentResponseDTO = new CommentResponseDTO
                {
                    //UserId = usId,
                    CommentID = model.CommentId,
                    ResponseDate = model.CreateDate,
                    UserName = MaskEmail(cevapVerenMail), // Maskelenmiş email
                    YorumCevapMetni = model.ResponseContent
                };

                return Json(new { success = true, response = commentResponseDTO });
            }
            else
            {
                return Json(new { success = false, message = "Yorum eklenirken bir hata oluştu." });
            }
        }

    }
}