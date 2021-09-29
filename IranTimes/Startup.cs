using IranTimes.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMarkupMin.AspNetCore3;

namespace NewShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region WebMarkup
            services.AddWebMarkupMin(options =>
            {
                options.AllowCompressionInDevelopmentEnvironment = true;
                options.AllowMinificationInDevelopmentEnvironment = true;
            })
              .AddHtmlMinification()
              .AddHttpCompression();
            #endregion

            services.AddControllersWithViews();

            #region Connection String      
            services.AddDbContext<NewCmsContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
                });
            #endregion

            #region IOC
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPageGroupRepository, PageGroupRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IMessageSender, MessageSender>();
            //services.AddIdentity<IdentityUser, IdentityRole>();
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(option => {

                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>()           
                .AddEntityFrameworkStores<NewCmsContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            #endregion
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseStaticFiles();
            app.UseWebMarkupMin();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                name: "Admin",
                areaName: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
