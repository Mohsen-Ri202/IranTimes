using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NewShop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IranTimes
{
    public class PageRepository : IPageRepository
    {
        private readonly NewCmsContext _context;
        private readonly IMemoryCache _cache;
        public PageRepository(NewCmsContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public List<Page> GetAllPages()
        {
            List<Page> pages;
            if (!_cache.TryGetValue("", out pages))
            {
                pages = _context.Pages.Include(x => x.PageGroup).OrderByDescending(o => o.CreateDate).ToList();
                _cache.Set("", pages, TimeSpan.FromMinutes(2));
            }
            return pages;
        }
        public void Insert(Page page)
        {
            _context.Pages.Add(page);
        }
        public void DeleteById(int id)
        {
            var page = _context.Pages.FirstOrDefault(p => p.id == id);
            PageDelete(page);
        }
        public void PageDelete(Page page)
        {
            _context.Pages.Remove(page);
        }

        public void PageUpdate(Page page)
        {
            _context.Entry(page).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Page GetPageById(int id)
        {
            var page = _context.Pages.Include(i => i.Comments).FirstOrDefault(p => p.id == id);
            return page;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Page> Search(string parameter)
        {
            return _context.Pages.Where(w => w.Text.Contains(parameter) ||
            w.PageGroup.GroupName.Contains(parameter) ||
            w.Title.Contains(parameter) ||
            w.ShortDescription.Contains(parameter));
        }

        public int PageCount()
        {
            return _context.Pages.Count();
        }
    }
}
