using WebAppSibers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebAppSibers.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Domain.Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>().HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            modelBuilder.Entity<Project>()
                .HasMany(p => p.EmployeeProjects)
                .WithOne(ep => ep.Project)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeProjects)
                .WithOne(ep => ep.Employee)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Domain.Models.Task>()
               .HasOne(t => t.Author)
               .WithMany()
               .HasForeignKey(t => t.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Models.Task>()
                .HasOne(t => t.Assignee)
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Models.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
