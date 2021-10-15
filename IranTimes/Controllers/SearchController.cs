using Microsoft.AspNetCore.Mvc;
using NewShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes
{
    public class SearchController : Controller
    {
        private readonly IPageRepository _pageRepository;
        public SearchController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public IActionResult Index(string q)
        {
            ViewBag.name = q;
           var model= _pageRepository.Search(q);
            return View(model);
        }
    }
}
