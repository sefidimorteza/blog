using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using weblog.CoreLayer.Services.Categories;
using weblog.CoreLayer.Services.IFileManager;
using weblog.CoreLayer.Services.Posts;
using weblog.CoreLayer.Services.User;
using weblog.DataLayer.Context;

namespace Myblog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();

            #region DataBase

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService, categoryServie>();
            builder.Services.AddScoped<IFileManager, FileManager>();
            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddDbContext<BlogContext>(Options =>
                Options.UseSqlServer(builder.Configuration
                    .GetConnectionString("Default")));
            #endregion

            #region authentication

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(option =>
            {
                option.LoginPath = "/Auth/Login";
                option.LogoutPath = "/Auth/Logout";
                option.ExpireTimeSpan = TimeSpan.FromDays(30);
            });
            #endregion

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
            });
            app.MapRazorPages();


            app.Run();
        }
    }
}
