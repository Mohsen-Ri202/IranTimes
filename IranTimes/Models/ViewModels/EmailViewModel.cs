using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IranTimes
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید ")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل شما معتبر نمی باشد")]
        public string Email { get; set; }
    }
}
