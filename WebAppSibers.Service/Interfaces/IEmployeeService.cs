using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppSibers.Domain.Models;
using WebAppSibers.Domain.Responce;

namespace WebAppSibers.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IBaseResponce<IEnumerable<Employee>>> GetEmployeesAsync();
        Task<IBaseResponce<Employee>> GetEmployeeByIdAsync(int id);
        Task<IBaseResponce<IEnumerable<Employee>>> FindEmployeesAsync(Expression<Func<Employee, bool>> predicate);
        Task<IBaseResponce<bool>> AddEmployeeAsync(Employee employee);
        Task<IBaseResponce<bool>> RemoveEmployeeByIdAsync(int id);
        Task<IBaseResponce<bool>> UpdateEmployeeAsync(Employee employee);
    }
}
