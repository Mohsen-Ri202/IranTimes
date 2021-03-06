using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewShop.Models
{
    public class PageGroup
    {
         [Key]
        public int Id { get; set; }
        [Display(Name ="نام گروه")]
        public string GroupName { get; set; }
        public virtual ICollection<Page> Page { get; set; }
    }
}
