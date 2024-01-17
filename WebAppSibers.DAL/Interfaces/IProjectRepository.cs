using System.Linq.Expressions;
using WebAppSibers.Domain.Models;

namespace WebAppSibers.DAL.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate);
        System.Threading.Tasks.Task AddAsync(Project entity);
        System.Threading.Tasks.Task UpdateAsync(Project entity);
        System.Threading.Tasks.Task RemoveAsync(Project entity);
    }
}
