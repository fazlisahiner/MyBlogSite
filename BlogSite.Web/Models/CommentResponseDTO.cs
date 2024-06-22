using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Web.Models
{
    public class CommentResponseDTO
    {
        public int CommentID { get; set; }
        public string YorumCevapMetni { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}