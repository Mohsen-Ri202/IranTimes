using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewShop
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public int PageID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Text { get; set; }
        public int? ParentID { get; set; }
        public DateTime CreateDate { get; set; }
        public  Page Page { get; set; }
        public Comment Parent { get; set; }
        public List<Comment> Child { get; set; }
    }
}
