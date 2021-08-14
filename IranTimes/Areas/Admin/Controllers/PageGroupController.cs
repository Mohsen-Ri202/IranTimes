using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop.Areas
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PageGroupController : Controller
    {
        private IPageGroupRepository _pageGroupRepository;
        private NewCmsContext _context;
        public PageGroupController(IPageGroupRepository pageGroupRepository, NewCmsContext context)
        {
            _pageGroupRepository = pageGroupRepository;
            _context = context;
        }
        // GET: PagesController.cs/Pages
        public IActionResult Index()
        {
            var page = _pageGroupRepository.GetAllGroups();
            return View(page);
        }

        // GET: PagesController.cs/Pages/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Group = _pageGroupRepository.GetGroupById(id.Value);
            if (Group == null)
            {
                return NotFound();
            }

            return View(Group);
        }

        // GET: PagesController.cs/Pages/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: PagesController.cs/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,GroupName")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {

                _pageGroupRepository.AddGroup(pageGroup);
                _pageGroupRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(pageGroup);
        }

        // GET: PagesController.cs/Pages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _pageGroupRepository.GetGroupById(id.Value);
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
        public IActionResult Edit(int id, [Bind("Id,GroupName")] PageGroup pageGroup)
        {
            if (id != pageGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (pageGroup != null)
                {
                    _pageGroupRepository.UpdatePageGroup(pageGroup);
                    _pageGroupRepository.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }

            }
            return View(pageGroup);
        }

        // GET: PagesController.cs/Pages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var pageGroup = _pageGroupRepository.GetGroupById(id.Value);
            //_pageGroupRepository.DeletePageGroup(pageGroup);
            _pageGroupRepository.DeleteById(id.Value);
            _pageGroupRepository.Save();
          
            return RedirectToAction(nameof(Index));

            //return View(pageGroup);
        }

        // POST: PagesController.cs/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pageGroupRepository.DeleteById(id);
            _pageGroupRepository.Save();
            return RedirectToAction(nameof(Index));
        }
       
       

    }
}
