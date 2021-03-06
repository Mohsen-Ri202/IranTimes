using System.ComponentModel.DataAnnotations;
namespace IranTimes
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name ="نام کاربری")]
        public string UserName { get; set; }
        public string Token { get; set; }
        [Required]
        [Display(Name ="رمز عبور")]
        public string Password { get; set; }
    }
}
