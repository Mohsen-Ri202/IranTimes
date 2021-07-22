using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
    public class SearchController : Controller
    {
        private IPageRepository _pageRepository;
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
