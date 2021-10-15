using System;
using System.Collections.Generic;

namespace NewShop
{
   public interface IPageRepository:IDisposable
    {
        public void Insert(Page page);
        public void PageUpdate(Page page);
        public void PageDelete(Page page);
        public void DeleteById(int id);
        public List<Page> GetAllPages();
        public Page GetPageById(int id);
        public IEnumerable<Page> Search(string parameter);
        public int PageCount();
        public void Save();

    }
}
