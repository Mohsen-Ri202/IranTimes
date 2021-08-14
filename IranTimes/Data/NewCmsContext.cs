using Microsoft.EntityFrameworkCore;
using NewShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewShop;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IranTimes;

namespace NewShop
{
    public class NewCmsContext:IdentityDbContext
    {
        public NewCmsContext(DbContextOptions<NewCmsContext> options):base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<NewShop.Createmodel> Createmodel { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Createmodel>(
                    eb =>
                    {
                        eb.HasNoKey();
                      
                       
                    });

        }
        public DbSet<IranTimes.RoleViewModel> RoleViewModel { get; set; }
    }
}
