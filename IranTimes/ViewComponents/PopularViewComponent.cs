using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
    public class PopularViewComponent: ViewComponent
    {
        private IPageRepository _pagerepository;
        public PopularViewComponent(IPageRepository pageRepository)
        {
            _pagerepository = pageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _pagerepository.GetAllPages().Where(w=>w.Visit>4).OrderByDescending(o=> o.Visit);
            return View (model);
        }
    }
}
