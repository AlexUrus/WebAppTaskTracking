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
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IBaseResponce<IEnumerable<Domain.Models.Task>>> GetTasksAsync()
        {
            var baseResponce = new BaseResponce<IEnumerable<Domain.Models.Task>>();

            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                baseResponce.Data = tasks;

                if (tasks?.Count() == 0)
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
                return new BaseResponce<IEnumerable<Domain.Models.Task>>()
                {
                    Description = $"[GetTasksAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<Domain.Models.Task>> GetTaskByIdAsync(int id)
        {
            var baseResponce = new BaseResponce<Domain.Models.Task>();

            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                baseResponce.Data = task;

                if (task == null)
                {
                    baseResponce.StatusCode = StatusCode.NotFound;
                    baseResponce.Description = "Задача не найдена";
                }
                else
                {
                    baseResponce.StatusCode = StatusCode.OK;
                }

                return baseResponce;
            }
            catch (Exception ex)
            {
                return new BaseResponce<Domain.Models.Task>()
                {
                    Description = $"[GetTaskByIdAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<Domain.Models.Task>>> FindTasksAsync(Expression<Func<Domain.Models.Task, bool>> predicate)
        {
            var baseResponce = new BaseResponce<IEnumerable<Domain.Models.Task>>();

            try
            {
                var tasks = await _taskRepository.FindAsync(predicate);
                baseResponce.Data = tasks;

                if (tasks?.Count() == 0)
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
                return new BaseResponce<IEnumerable<Domain.Models.Task>>()
                {
                    Description = $"[FindTasksAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> AddTaskAsync(Domain.Models.Task task)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                await _taskRepository.AddAsync(task);
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
                    Description = $"[AddTaskAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> RemoveTaskByIdAsync(int id)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                var employee = await _taskRepository.GetByIdAsync(id);
                if (employee != null)
                {
                    await _taskRepository.RemoveAsync(employee);
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
                    Description = $"[RemoveTaskByIdAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> UpdateTaskAsync(Domain.Models.Task task)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                await _taskRepository.UpdateAsync(task);
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
                    Description = $"[UpdateTaskAsync] : {ex.Message}"
                };
            }
        }
    }
}
