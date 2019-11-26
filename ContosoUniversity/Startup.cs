using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Services;

namespace ContosoUniversity
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Added in Week 1 Exercise, 16/7/2019
            services.AddDbContext<BookStoreContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.CookieHttpOnly = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            await CreateRoles(serviceProvider);
        }

        public async Task CreateRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var apContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                apContext.Database.EnsureCreated();

                //if there is already an Admin role, abort
                var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                string[] roleNames = { "Admin", "Member" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    bool roleExist = _roleManager.RoleExistsAsync(roleName).Result;
                    if (!roleExist)
                    {
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var powerUser = new ApplicationUser
                {
                    UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                    Email = Configuration.GetSection("UserSettings")["UserEmail"],
                    Address = "Admin Address",
                    EmailConfirmed = true,
                    Enabled = true
                };

                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var test = _userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);
                if (test.Result == null)
                {
                    string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
                    powerUser.EmailConfirmed = true;
                    var createPowerUser = await _userManager.CreateAsync(powerUser, userPassword);

                    if (createPowerUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(powerUser, "Admin");
                    }
                }
            }
        }
    }
}
