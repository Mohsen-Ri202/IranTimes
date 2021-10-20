using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewShop;
using System.IO;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using IranTimes.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IranTimes.Areas
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly NewCmsContext _context;
        private readonly IPageRepository _pageRepository;
        private readonly IPageGroupRepository _pageGroupRepository;      
        public HomeController(NewCmsContext context, IPageRepository pageRepository
        , IPageGroupRepository pageGroupRepository)
        {

            _pageGroupRepository = pageGroupRepository;
            _context = context;
            _pageRepository = pageRepository;        
        }

        [AllowAnonymous]
        // GET: PagesController.cs/Pages
        public IActionResult Index()
        {
            List<Page> page = _pageRepository.GetAllPages();
            return View(page);
        }

        // GET: PagesController.cs/Pages/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: PagesController.cs/Pages/Create

        public IActionResult Create()
        {
            var groups = _pageGroupRepository.GetAllGroups();
            CreateViewModel model = new CreateViewModel() { PageGroups = groups };
            return View(model);
        }

        // POST: PagesController.cs/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,Title,PageGroupId,ShortDescription,Text,Visit,ShowInSlider,ImageName,CreateDate")] CreateViewModel model, IFormFile ImgUp)
        {
            if (ModelState.IsValid)
            {
                if (ImgUp != null)
                {
                    var FileName = Guid.NewGuid() + Path.GetExtension(ImgUp.FileName);
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageFile", FileName);
                    using (var FileStream = new FileStream(SavePath, FileMode.Create))
                    {
                        ImgUp.CopyTo(FileStream);

                    }
                    model.ImageName = FileName;
                }
                model.CreateDate = DateTime.Now;

                Page page = new Page()
                {
                    ImageName = model.ImageName,
                    PageGroupId = model.PageGroupId,                    
                    ShortDescription = model.ShortDescription,
                    ShowInSlider = model.ShowInSlider,
                    Text = model.Text,
                    Title = model.Title,
                    CreateDate = model.CreateDate,
                };

                _pageRepository.Insert(page);
                _pageRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PagesController.cs/Pages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: PagesController.cs/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,Title,PageGroupId,ShortDescription,Text,Visit,ShowInSlider,ImageName,CreateDate")] Page page, IFormFile ImgUp)
        {
            if (id != page.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    if (ImgUp != null)
                    {
                        var newFileName = Guid.NewGuid() + Path.GetExtension(ImgUp.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/ImageFile", newFileName);
                        using (var fileStream = new FileStream(savePath, FileMode.Create))
                        {
                            ImgUp.CopyTo(fileStream);
                        }

                        page.ImageName = newFileName;
                    }
                    _pageRepository.PageUpdate(page);
                    _pageRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: PagesController.cs/Pages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var page = _pageRepository.GetPageById(id.Value);
            if (page.ImageName != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imageFile", page.ImageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    _pageRepository.PageDelete(page);
                }
               
            }
            else
            {
                _pageRepository.PageDelete(page);
            }
          

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: PagesController.cs/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pageRepository.DeleteById(id);
            _pageRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.id == id);
        }

    }
}
