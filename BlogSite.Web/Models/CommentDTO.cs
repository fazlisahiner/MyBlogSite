using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Web.Models
{
    public class CommentDTO
    {
        public int CommetId { get; set; }
        public int ArticleId { get; set; }//Article
        public string CommentContent{ get; set; }//Comment
        public int? UserId { get; set; }
        public string  UserName{ get; set; }
        public DateTime CommentDatetime { get; set; }
        //public string UserName { get; set; }


    }
}