using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppSibers.DAL.Interfaces;
using WebAppSibers.DAL.Repository;
using WebAppSibers.Domain.Enum;
using WebAppSibers.Domain.Models;
using WebAppSibers.Domain.Responce;
using WebAppSibers.Service.Interfaces;

namespace WebAppSibers.Service.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IBaseResponce<IEnumerable<Project>>> GetProjectsAsync()
        {
            var baseResponce = new BaseResponce<IEnumerable<Project>>();

            try
            {
                var employees = await _projectRepository.GetAllAsync();
                baseResponce.Data = employees;

                if (employees?.Count() == 0)
                {
                    baseResponce.Description = "Найдено 0 элементов";
                    baseResponce.StatusCode = StatusCode.NotFound;
                }
                else
                {
                    baseResponce.StatusCode = StatusCode.OK;
                }

                return baseResponce;
            }
            catch (Exception ex)
            {
                return new BaseResponce<IEnumerable<Project>>()
                {
                    Description = $"[GetProjectsAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<Project>> GetProjectByIdAsync(int id)
        {
            var baseResponce = new BaseResponce<Project>();

            try
            {
                var employee = await _projectRepository.GetByIdAsync(id);
                baseResponce.Data = employee;

                if (employee == null)
                {
                    baseResponce.StatusCode = StatusCode.NotFound;
                    baseResponce.Description = "Проект не найден";
                }
                else
                {
                    baseResponce.StatusCode = StatusCode.OK;
                }

                return baseResponce;
            }
            catch (Exception ex)
            {
                return new BaseResponce<Project>()
                {
                    Description = $"[GetProjectByIdAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<Project>>> FindProjectsAsync(Expression<Func<Project, bool>> predicate)
        {
            var baseResponce = new BaseResponce<IEnumerable<Project>>();

            try
            {
                var employees = await _projectRepository.FindAsync(predicate);
                baseResponce.Data = employees;

                if (employees?.Count() == 0)
                {
                    baseResponce.Description = "Найдено 0 элементов";
                    baseResponce.StatusCode = StatusCode.NotFound;
                }
                else
                {
                    baseResponce.StatusCode = StatusCode.OK;
                }

                return baseResponce;
            }
            catch (Exception ex)
            {
                return new BaseResponce<IEnumerable<Project>>()
                {
                    Description = $"[FindProjectsAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> AddProjectAsync(Project project)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                await _projectRepository.AddAsync(project);
                baseResponce.Data = true;
                baseResponce.StatusCode = StatusCode.OK;

                return baseResponce;
            }
            catch (Exception ex)
            {
                baseResponce.Data = false;
                baseResponce.StatusCode = StatusCode.InternalServerError;
                return new BaseResponce<bool>()
                {
                    Description = $"[AddProjectAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> RemoveProjectByIdAsync(int id)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                var employee = await _projectRepository.GetByIdAsync(id);
                if (employee != null)
                {
                    await _projectRepository.RemoveAsync(employee);
                    baseResponce.Data = true;
                    baseResponce.StatusCode = StatusCode.OK;
                }
                else
                {
                    baseResponce.Data = false;
                    baseResponce.StatusCode = StatusCode.NotFound;
                }

                return baseResponce;
            }
            catch (Exception ex)
            {
                baseResponce.Data = false;
                baseResponce.StatusCode = StatusCode.InternalServerError;
                return new BaseResponce<bool>()
                {
                    Description = $"[RemoveProjectByIdAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> UpdateProjectAsync(Project project)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                await _projectRepository.UpdateAsync(project);
                baseResponce.Data = true;
                baseResponce.StatusCode = StatusCode.OK;

                return baseResponce;
            }
            catch (Exception ex)
            {
                baseResponce.Data = false;
                baseResponce.StatusCode = StatusCode.InternalServerError;
                return new BaseResponce<bool>()
                {
                    Description = $"[UpdateProjectAsync] : {ex.Message}"
                };
            }
        }
    }
}
