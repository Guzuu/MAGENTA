using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Magenta.DAL;
using Microsoft.AspNetCore.Identity;

namespace Magenta
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
            services.AddControllersWithViews();

            services.AddDbContext<DefaultContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultContext")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DefaultContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            // Initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;

            // Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("OfficeWorker");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("OfficeWorker"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("GraphicDesigner");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("GraphicDesigner"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("BookBinder");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("BookBinder"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("Printer");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Printer"));
            }

            // Assign Admin role to newly registered user
            IdentityUser user = await UserManager.FindByEmailAsync("Admin@nonexistingdomain.pl");
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
