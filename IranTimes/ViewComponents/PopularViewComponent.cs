using Microsoft.AspNetCore.Mvc;
using NewShop;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes
{
    public class PopularViewComponent: ViewComponent
    {
        private readonly IPageRepository _pagerepository;
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
