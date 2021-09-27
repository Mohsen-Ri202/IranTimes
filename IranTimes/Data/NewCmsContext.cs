using Microsoft.EntityFrameworkCore;
using NewShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IranTimes.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace NewShop
{
    public class NewCmsContext:IdentityDbContext<ApplicationUser>
    {
        public NewCmsContext(DbContextOptions<NewCmsContext> options):base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Createmodel> Createmodel { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Createmodel>(
                    eb =>
                    {
                        eb.HasNoKey();                                     
                    });
            modelBuilder.Entity<IdentityUser>()
                .HasDiscriminator<int>("Type")
                .HasValue<IdentityUser>(0)
                .HasValue<ApplicationUser>(1);

            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");
            //modelBuilder.Entity<ApplicationUser>()
            //    .Property(p => p.IsPayed)
            //    .HasDefaultValue(false);       
        } 
        public DbSet<IranTimes.RoleViewModel> RoleViewModel { get; set; }
    }
}
