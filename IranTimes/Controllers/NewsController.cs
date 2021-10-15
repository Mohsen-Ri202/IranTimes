using Microsoft.AspNetCore.Mvc;
using NewShop;
using System;

namespace IranTimes
{
    public class NewsController : Controller
    {
        private IPageRepository _pageRepository;
        private ICommentRepository _commentrepository;
        public NewsController(IPageRepository pageRepository,ICommentRepository commentRepository)
        {
            _pageRepository = pageRepository;
            _commentrepository = commentRepository;
        }
        public IActionResult ShowNews(int id)
        {
            var model = _pageRepository.GetPageById(id);
            model.Visit += 1;
            _pageRepository.Save();
            return View(model);
        }
        public IActionResult ShowGroups()
        {
            return View();
        }
        public IActionResult Comment(int id, string name, string text, string website, string email)
        {
            
                Comment newcomment = new Comment()
                {
                    Email = email,
                    Name = name,
                    Text = text,
                    WebSite = website,
                    PageID = id,
                    CreateDate = DateTime.Now
                }; 
                _commentrepository.AddComment(newcomment);
                _commentrepository.Save();
            return Json(newcomment);       
        }
     
    }
}
