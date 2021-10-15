using Microsoft.AspNetCore.Mvc;
using NewShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes
{
    public class ShowCommentViewComponent:ViewComponent
    {
        private readonly ICommentRepository _commentRepository;
        public ShowCommentViewComponent(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Page page)
        {
            var model =  _commentRepository.GetCommentById(page.id);
            return View(model);

        }
    }
}
