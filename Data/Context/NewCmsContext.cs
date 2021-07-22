using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    class NewCmsContext:DbContext
    {
        public NewCmsContext(DbContextOptions<NewCmsContext> options):base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageGroup> PageGroups { get; set; }
    }
}
