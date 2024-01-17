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
    public interface IProjectService
    {
        Task<IBaseResponce<IEnumerable<Project>>> GetProjectsAsync();
        Task<IBaseResponce<Project>> GetProjectByIdAsync(int id);
        Task<IBaseResponce<IEnumerable<Project>>> FindProjectsAsync(Expression<Func<Project, bool>> predicate);
        Task<IBaseResponce<bool>> AddProjectAsync(Project project);
        Task<IBaseResponce<bool>> RemoveProjectByIdAsync(int id);
        Task<IBaseResponce<bool>> UpdateProjectAsync(Project project);
    }
}
