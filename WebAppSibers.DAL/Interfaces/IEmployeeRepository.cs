using System.Linq.Expressions;
using WebAppSibers.Domain.Models;

namespace WebAppSibers.DAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate);
        System.Threading.Tasks.Task AddAsync(Employee entity);
        System.Threading.Tasks.Task UpdateAsync(Employee entity);
        System.Threading.Tasks.Task RemoveAsync(Employee entity);
    }
}
