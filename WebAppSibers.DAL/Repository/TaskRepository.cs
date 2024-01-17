using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppSibers.DAL.Interfaces;

namespace WebAppSibers.DAL.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Models.Task>> GetAllAsync()
        {
            return await _context.Tasks.Include(t => t.Author)
                                           .Include(t => t.Assignee)
                                           .Include(t => t.Project)
                                           .ToListAsync();
        }

        public async Task<Domain.Models.Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Models.Task>> FindAsync(Expression<Func<Domain.Models.Task, bool>> predicate)
        {
            return await _context.Tasks.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Domain.Models.Task entity)
        {
            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Models.Task entity)
        {
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Domain.Models.Task entity)
        {
            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
