using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IranTimes
{
    public class RoleViewModel
    {
       
        public string id { get; set; }
        [Required]
        [Display(Name = "نام مقام")]
        public string  RoleName { get; set; }
    }
}
