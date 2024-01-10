using Booky.BL.interfaces;
using Booky.BL.Repository;
using Booky.DAL.Data;
using Booky.DAL.Migrations;
using Booky.DAL.Models;
using Booky.PL.MappingProfile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Booky.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            #region Application services with DI
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BookyDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            builder.Services.AddAutoMapper(m=>
            {
                m.AddProfile<ProductProfile>();
                m.AddProfile<UserProfile>();
                m.AddProfile<RoleProfile>();

            }
            );
           
            builder.Services.AddIdentity<DAL.Models.ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit= true;
                options.Password.RequireLowercase= false;
                options.Password.RequireUppercase= false;
                options.Password.RequireNonAlphanumeric= false;
            }).AddEntityFrameworkStores<BookyDbContext>()
            .AddDefaultTokenProviders();
            //////
            //builder.Services.AddIdentity<ApplicationRole, ApplicationUser>(options =>
            //{

            //}).AddEntityFrameworkStores<BookyDbContext>()
            //.AddDefaultTokenProviders();
            //////
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath ="/Security/Security/Login";
            });

            #endregion
            var app = builder.Build();

            #region Application request pipeline
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}