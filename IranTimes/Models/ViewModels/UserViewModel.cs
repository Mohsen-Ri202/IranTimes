using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace NewShop
{
    public class UserViewModel
    {
        [Remote("IsExistUser", "Account",HttpMethod ="POST",AdditionalFields = "__RequestVerificationToken")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(150)]
        public string UserName { get; set; }

        [Remote("IsExistEmail", "Account", HttpMethod = "POST",AdditionalFields ="__RequestVerificationToken")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
