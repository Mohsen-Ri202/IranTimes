using IranTimes;
using IranTimes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NewShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageSender _messageSender;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMessageSender messageSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messageSender = messageSender;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserName,Email,Password,RePassword")] UserViewModel model)
        {
            var user = new IdentityUser()
            {
               UserName=model.UserName,
               Email=model.Email,            
            };
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var emailMessage = Url.Action("ConfirmEmail", "Account", new { username = user.UserName, token = emailConfirmationToken }
                    , Request.Scheme);

                    await _messageSender.SendEmailAsync(model.Email, "EmailConfirm", emailMessage);
                    return View("RegisterSuccess");

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            
            if (  _signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index");
            }
            return  View();
        } 
        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel model)
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RemmberMe, true);
                
                if (result.Succeeded) return RedirectToAction("Index");


                if (result.IsLockedOut) ViewData["ErrorMessage"] = "اکانت شما قفل شده است";

            }
            ModelState.AddModelError("","اطلاعات نا معتبر است");
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string username , string token)
        {
            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(token)) return NotFound();
          
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);
           
            if (result.Succeeded) return View("/Views/Account/EmailConfirm.cshtml");
            else return Content("NotConfirmd");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsExistUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return Json(true);          
            else return Json("این کاربر قبلا ثبت نام کرده است");
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsExistEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user==null)
            {
                return Json(true);
            }
            else
            {
                return Json("این ایمیل قبلا ثبت شده");
            }
        }

        //This action takes the user's email and send reset password link to that
        public  IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {

                
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "کاربری با این ایمیل یافت نشد");
                    return View();
                }

                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var emailMessage = Url.Action("ResetPassword", "Account", new { username = user.UserName, token = resetPasswordToken }
                , Request.Scheme);
                await _messageSender.SendEmailAsync(model.Email, "ResetEmail", emailMessage);
                return View("/Views/Account/SendEmailSuccess.cshtml");
            }
                return View(model.Email);
            
        }

        //This takes the new user's password and replaces the previous one
        public IActionResult ResetPassword(string username,string token)
        {
            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(token)) return NotFound();
        
            
            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                 UserName=username,
                 Token=token,
                 Password=""
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword( [Bind("UserName,Token,Password")] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var user = await _userManager.FindByNameAsync(model.UserName);                
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
               
                if (result.Succeeded) return View("/Views/Account/ResetSuccess.cshtml");           
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);            
        }


    }
}
