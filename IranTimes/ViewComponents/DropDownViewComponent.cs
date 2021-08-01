using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
    public class DropDownViewComponent:ViewComponent
    {
        private NewCmsContext _context;
        public DropDownViewComponent(NewCmsContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =  _context.PageGroups;
            return View(model);
        }
    }
}
