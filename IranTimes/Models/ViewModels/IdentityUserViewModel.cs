using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IranTimes
{
    public class IdentityUserViewModel
    {
        public string Id { get; set; }
        [Display(Name ="نام کاربر :")]
        public string Name { get; set; }

    }
}
