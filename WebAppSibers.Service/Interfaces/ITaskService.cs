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
    public interface ITaskService
    {
        Task<IBaseResponce<IEnumerable<Domain.Models.Task>>> GetTasksAsync();
        Task<IBaseResponce<Domain.Models.Task>> GetTaskByIdAsync(int id);
        Task<IBaseResponce<IEnumerable<Domain.Models.Task>>> FindTasksAsync(Expression<Func<Domain.Models.Task, bool>> predicate);
        Task<IBaseResponce<bool>> AddTaskAsync(Domain.Models.Task task);
        Task<IBaseResponce<bool>> RemoveTaskByIdAsync(int id);
        Task<IBaseResponce<bool>> UpdateTaskAsync(Domain.Models.Task task);
    }
}
