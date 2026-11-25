using System.Diagnostics;
using Assinment_MVC2.Data;
using Microsoft.EntityFrameworkCore;

namespace Assinment_MVC2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
                context.Database.Migrate();
                PlanSeeder.Seed(context, app.Environment.ContentRootPath);
            }

            if (app.Environment.IsDevelopment())
            {
                app.Lifetime.ApplicationStarted.Register(() =>
                {
                    var address = app.Urls.FirstOrDefault(url => url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        ?? app.Urls.FirstOrDefault()
                        ?? "http://localhost:5257";

                    Process.Start(new ProcessStartInfo(address.TrimEnd('/') + "/Plan")
                    {
                        UseShellExecute = true
                    });
                });
            }

            app.Run();
        }
    }
}
