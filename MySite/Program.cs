using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Services;
using MySite.Services.ServicesForEditing;
using MySite.Services.ServicesForHome;
using MySite.Services.ServicesForLibrary;
using MySite.Services.ServicesForSelection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MySite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/index");

            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<DbVideoGamesContext>(options
                => options.UseNpgsql("Server=localhost; DataBase=db_video_games; User Id=postgres;password = 1234"));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddTransient<IAddingGameService, AddingGame>();
            builder.Services.AddTransient<ILibraryService, LibraryService>();
            builder.Services.AddTransient<IHomeService, HomeService>();
            builder.Services.AddTransient<IEditingService,EditingService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
                pattern: "{controller=Home}/{action=Index}/{id?}");
            


            app.Run();
            
        }
    }
}
