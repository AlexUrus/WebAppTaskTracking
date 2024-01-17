using System.Linq.Expressions;

namespace WebAppSibers.DAL.Interfaces
{
    public interface ITaskRepository : IRepository<Domain.Models.Task>
    {
        Task<IEnumerable<Domain.Models.Task>> GetAllAsync();
        Task<Domain.Models.Task> GetByIdAsync(int id);
        Task<IEnumerable<Domain.Models.Task>> FindAsync(Expression<Func<Domain.Models.Task, bool>> predicate);
        Task AddAsync(Domain.Models.Task entity);
        Task UpdateAsync(Domain.Models.Task entity);
        Task RemoveAsync(Domain.Models.Task entity);
    }
}
