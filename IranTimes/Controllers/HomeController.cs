using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NewShop.Models;

using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using ZarinpalSandbox;
using ZarinPal;
using IranTimes.Models;
using System.Threading.Tasks;

namespace NewShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPageRepository _pageRepository;
        private readonly NewCmsContext _context;
        private readonly IMemoryCache _cache;
        public HomeController(ILogger<HomeController> logger,
            NewCmsContext context,
            IPageRepository pageRepository,
            IMemoryCache cache)
        {
            _logger = logger;
            _context = context;
            _pageRepository = pageRepository;
            _cache = cache;
        }

        public IActionResult Index(int pageid=1)
        {           
            int skip = (pageid - 1) * 3;
            int count = _context.Pages.Count();
            ViewBag.PageCount = count/3;
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
        public IActionResult Payment()
        {
            var price = 1500;
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);           
            if (userName == null) return NotFound();
            var payment = new Payment(price);
            var result = payment.PaymentRequest($"پرداخت حق اشتراک{userName}", "http://localhost:10612/Home/OnlinePayment/" + userId, email, "09904848916");
            if (result.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }
            else return BadRequest();           
        }
        public async Task<IActionResult> OnlinePayment(string userId)
        {

            if (HttpContext.Request.Query["Status"]!=""&&
                HttpContext.Request.Query["Status"].ToString().ToLower()=="ok" &&
                HttpContext.Request.Query["Authority"] !="")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var user = await _userManager.FindByIdAsync(userId.ToString());
                
                var payment = new Payment(1500);
                var result = payment.Verification(authority).Result;
                if (result.Status==100)
                {
                   
                    user.IsPayed = true;
                    return RedirectToAction("/");
                }

                return NotFound();

            }
            return RedirectToAction("/");
        }
    
    }
}
