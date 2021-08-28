using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NewShop.Models;

using System.Diagnostics;
using System.Linq;

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
    }
}
