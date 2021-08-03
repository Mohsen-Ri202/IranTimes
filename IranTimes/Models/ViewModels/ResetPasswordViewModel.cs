using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IranTimes
{
    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        [Required]
        [Display(Name ="رمز عبور")]
        public string Password { get; set; }
    }
}
