using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Web.Models
{
    public class Listeler
    {
        public List<Users> Userslar { get; set; }
        public List<Categories> Categoriesler { get; set; }
        public Articles Makale {  get; set; }

    }
}