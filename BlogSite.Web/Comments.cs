//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlogSite.Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int CommetId { get; set; }
        public int ArticleId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Content { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string UserName { get; set; }
    }
}
