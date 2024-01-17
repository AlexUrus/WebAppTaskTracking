using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppSibers.DAL.Interfaces;
using WebAppSibers.Domain.Models;

namespace WebAppSibers.DAL.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(x => x.ProjectManager)
                .Include(x => x.Tasks)
                .Include(x => x.EmployeeProjects)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(x => x.ProjectManager)
                .Include(x => x.Tasks)
                .Include(x => x.EmployeeProjects)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate)
        {
            return await _context.Projects.Where(predicate).ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Project entity)
        {
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Project entity)
        {
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task RemoveAsync(Project entity)
        {
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
