﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop.Controllers
{
    public class Account : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IMessageSender _messageSender;
        public Account(UserManager<IdentityUser> userManager,
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
            var result = await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var emailMessage = Url.Action("ConfirmEmail", "Account", new { username = user.UserName, token = emailConfirmationToken }
                ,Request.Scheme);
                await _messageSender.SendEmailAsync(model.Email, "EmailConfirm", emailMessage);
                return View("RegisterSuccess");

            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index");
            }
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel model)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RemmberMe, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                if (result.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "اکانت شما قفل شده است";
                }
            }
            ModelState.AddModelError("","اطلاعات نا معتبر است");
            return View();
        }
        public async Task<IActionResult> IsExistUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user==null)
            {
                return Json(true);
            }
            else
            {
                return Json("این کاربر قبلا ثبت نام کرده است");
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string username , string token)
        {
            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
              return  Content("confirm");
            }
            else
            {
              return  Content("NotConfirmd");
            }
        }
    }
}