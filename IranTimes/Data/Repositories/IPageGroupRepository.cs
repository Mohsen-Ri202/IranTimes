using NewShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShop
{
   public interface IPageGroupRepository:IDisposable
    {
        public void AddGroup(PageGroup pageGroup);
        public void UpdatePageGroup(PageGroup pageGroup);
        public void DeletePageGroup(PageGroup pageGroup);
        public void DeleteById(int id);
        public IEnumerable<PageGroup> GetAllGroups();
        public PageGroup GetGroupById(int id);
        public void Save();
        
    }
}
