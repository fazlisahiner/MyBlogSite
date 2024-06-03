using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Web.Areas.AdminPanel.Models
{
    public class UserDetay
    {
        //public int UsersId { get; set; }
        //public string UserName { get; set; }
        //public string UserPassword { get; set; }
        //public string EMail { get; set; }
        //public int RoleId { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime CreateDate { get; set; }
        public Users User { get; set; }
        public List<Roles> Roles { get; set; }
        public Roles Role { get; set; }
    }
}