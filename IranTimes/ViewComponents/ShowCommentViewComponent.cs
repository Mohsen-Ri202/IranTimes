using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
    public class ShowCommentViewComponent:ViewComponent
    {
        private ICommentRepository _commentRepository;
        public ShowCommentViewComponent(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Page page)
        {
            var model = _commentRepository.GetCommentById(page.id);
            return View(model);

        }
    }
}
