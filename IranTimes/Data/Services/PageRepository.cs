using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
    public class PageRepository : IPageRepository
    {
        private NewCmsContext _context;
        public PageRepository(NewCmsContext context)
        {
            _context = context;
        }
        public List<Page> GetAllPages()
        {
            return _context.Pages.Include(x => x.PageGroup).ToList();
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
          var page=  _context.Pages.Include(i=>i.Comments).FirstOrDefault(p=>p.id==id);
            return page;
        }

        public void Save()
        {
           _context.SaveChanges();
        }

        public IEnumerable<Page> Search(string parameter)
        {
           return _context.Pages.Where(w=> w.Text.Contains(parameter) || 
           w.PageGroup.GroupName.Contains(parameter)||
           w.Title.Contains(parameter) ||
           w.ShortDescription.Contains(parameter));
        }
    }
}
