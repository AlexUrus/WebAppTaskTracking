using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppSibers.DAL;
using WebAppSibers.DAL.Interfaces;
using WebAppSibers.DAL.Repository;
using WebAppSibers.Service.Implementations;
using WebAppSibers.Service.Interfaces;

namespace WebAppSibers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IServiceCollection services = builder.Services;

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();

            InitRepositories(services);
            InitServices(services);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
            name: "account",
            pattern: "{controller=Task}/{action=Index}");

            app.Run();
        }

        private static void InitRepositories(IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }

        private static void InitServices(IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
        }

    }
}