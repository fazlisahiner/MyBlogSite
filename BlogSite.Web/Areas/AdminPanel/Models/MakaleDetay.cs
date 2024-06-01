using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogSite.Web.Areas.AdminPanel.Models
{
    public class MakaleDetay
    {

       
        public Users User { get; set; }
        public Categories Category { get; set; }
        [AllowHtml]
        public Articles Article { get; set; }
        public List<Users> Users { get; set; }
        public List<Categories> Categories { get; set; }


    }
}