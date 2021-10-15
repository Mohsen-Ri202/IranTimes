using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using ZarinpalSandbox;
using IranTimes.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NewShop;

namespace IranTimes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPageRepository _pageRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ILogger<HomeController> logger,
            IPageRepository pageRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _pageRepository = pageRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(int pageid = 1)
        {
            int skip = (pageid - 1) * 3;
            int count = _pageRepository.PageCount();
            ViewBag.PageCount = count / 3;
            ViewBag.PageId = pageid;
            ViewBag.next = pageid += 1;
            var model = _pageRepository.GetAllPages()
             .Skip(skip)
             .Take(3);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Ruls()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Membership()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user.IsPayed == true)
            {
                return View("MemberShipRight");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Membership(int price)
        {
            return  Payment(price);
        }

        public IActionResult Payment(int price)
        {

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (userName == null) return NotFound();
            var payment = new Payment(price);
            var result = payment.PaymentRequest($"پرداخت حق اشتراک{userName}", "http://localhost:10612/Home/OnlinePayment/" + userId + "/" + price, email, "09904848916");
            if (result.Result.Status == 100) return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            else return BadRequest();
        }
        [Route("Home/OnlinePayment/{userId}/{price}")]
        public async Task<IActionResult> OnlinePayment(string userId, int price)
        {
            if (userId == null) return NotFound();

            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var user = await _userManager.FindByIdAsync(userId);
                var payment = new Payment(price);
                var result = payment.Verification(authority).Result;
                if (result.Status == 100)
                {
                    user.IsPayed = true;
                    await _userManager.AddToRoleAsync(user, "Admin");                   
                    await _userManager.UpdateAsync(user);
                    return View("PaymentSuccess");
                }
                return NotFound();
            }
            return View("PaymentFaild");
        }

        public IActionResult NewsLetter()
        {
            return View();
        }
    }
}
