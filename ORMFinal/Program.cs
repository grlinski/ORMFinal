using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ORMFinal.DAL;
using ORMFinal.BLL;

namespace ORMFinal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure logging
            builder.Logging.ClearProviders(); // Clear default providers
            builder.Logging.AddConsole(); // Add console logging
            builder.Logging.AddDebug(); // Add debug logging
            builder.Logging.SetMinimumLevel(LogLevel.Debug); // Set minimum log level

            //Register DB Context
            builder.Services.AddDbContext<ORMFinalContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });





            //// /////
            builder.Services.AddTransient<ExhibitService>();
            builder.Services.AddTransient<ExhibitDAL>();

            builder.Services.AddTransient<EmployeeDAL>();
            builder.Services.AddTransient<EmployeeService>();

            builder.Services.AddTransient<AnimalService>();
            builder.Services.AddTransient<AnimalDAL>();

            builder.Services.AddScoped<AnimalHealthDAL>();
            builder.Services.AddScoped<AnimalHealthService>();

            builder.Services.AddScoped<AnimalDAL>();
            builder.Services.AddScoped<AnimalService>();

            builder.Services.AddScoped<FeedingScheduleDAL>();
            builder.Services.AddScoped<FeedingScheduleService>();









            var app = builder.Build();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Application started zzyzz.");
            Console.WriteLine("Application started zzyzz.");

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
