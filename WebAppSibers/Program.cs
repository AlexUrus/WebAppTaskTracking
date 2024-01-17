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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            AddIdentity(builder.Services);
            var app = builder.Build();

            Configure(app);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "account",
            pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        private static void Configure(WebApplication app)
        {
            var host = app.Services.GetRequiredService<IHostApplicationLifetime>();

            host.ApplicationStarted.Register(async () =>
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                        await InitializeAsync(userManager, roleManager);
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
            });
        }

        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Leader", "ProjectManager", "Employee" };

            IdentityResult roleResult;

            try
            {
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            IdentityUser leader = new IdentityUser
            {
                UserName = "leader@example.com",
                Email = "leader@example.com",
            };

            IdentityUser projectManager = new IdentityUser
            {
                UserName = "manager@example.com",
                Email = "manager@example.com",
            };

            IdentityUser employee = new IdentityUser
            {
                UserName = "employee@example.com",
                Email = "employee@example.com",
            };

            await userManager.CreateAsync(leader, "Password123!");
            await userManager.AddToRoleAsync(leader, "Leader");

            await userManager.CreateAsync(projectManager, "Password123!");
            await userManager.AddToRoleAsync(projectManager, "ProjectManager");

            await userManager.CreateAsync(employee, "Password123!");
            await userManager.AddToRoleAsync(employee, "Employee");
        }
    }
}