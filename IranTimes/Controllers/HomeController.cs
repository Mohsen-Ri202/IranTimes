using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPageRepository _pagerepository;
        private NewCmsContext _Context;
        public HomeController(ILogger<HomeController> logger,NewCmsContext context,IPageRepository pageRepository)
        {
            _logger = logger;
            _Context = context;
            _pagerepository = pageRepository;
        }

        public IActionResult Index(int pageid=1)
        {           
            int skip = (pageid - 1) * 3;
            int count = _Context.Pages.Count();
            ViewBag.PageCount = count/3;
            ViewBag.PageId = pageid;
            ViewBag.next = pageid += 1;
            var model = _Context.Pages.Include(i=>i.PageGroup)
                .OrderByDescending(o=>o.CreateDate)
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
