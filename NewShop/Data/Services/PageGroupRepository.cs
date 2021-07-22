using NewShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewShop
{
    public class PageGroupRepository : IPageGroupRepository
    {
        private NewCmsContext _Context;
        public PageGroupRepository(NewCmsContext context)
        {
            _Context = context;
        }
        public void AddGroup(PageGroup pageGroup)
        {
              _Context.PageGroups.Add(pageGroup);
        }

        public void DeletePageGroup(PageGroup pageGroup)
        {
            _Context.PageGroups.Remove(pageGroup);
        }

        public IEnumerable<PageGroup> GetAllGroups()
        {
          var Group= _Context.PageGroups;
            return Group;
        }

        public void UpdatePageGroup(PageGroup pageGroup)
        {
            _Context.Entry(pageGroup).State=EntityState.Modified;

        }
        public void Dispose()
        {
            _Context.Dispose();
        }

        public PageGroup GetGroupById(int id)
        {
            var Group=_Context.PageGroups.SingleOrDefault(p=>p.Id==id);
            return Group;
        }

        public void Save()
        {
            _Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
         var Group= _Context.PageGroups.SingleOrDefault(p=>p.Id==id);
            DeletePageGroup(Group);
        }
    }
}
