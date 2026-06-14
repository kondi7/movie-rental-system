using MovieRentalSystem.Data;

namespace MovieRentalSystem;

using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<VideoRentalContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // if (!app.Environment.IsDevelopment())
        // {
        //     app.UseExceptionHandler("/Home/Error");
        //     app.UseHsts();
        // }

        //app.UseHttpsRedirection();
        app.UseRouting();

        //app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
