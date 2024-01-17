using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppSibers.DAL.Interfaces;
using WebAppSibers.Domain.Enum;
using WebAppSibers.Domain.Models;
using WebAppSibers.Domain.Responce;
using WebAppSibers.Service.Interfaces;

namespace WebAppSibers.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IBaseResponce<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            var baseResponce = new BaseResponce<IEnumerable<Employee>>();

            try
            {
                var employees = await _employeeRepository.GetAllAsync();
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
                return new BaseResponce<IEnumerable<Employee>>()
                {
                    Description = $"[GetEmployeesAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<Employee>> GetEmployeeByIdAsync(int id)
        {
            var baseResponce = new BaseResponce<Employee>();

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                baseResponce.Data = employee;

                if (employee == null)
                {
                    baseResponce.StatusCode = StatusCode.NotFound;
                    baseResponce.Description = "Сотрудник не найден";
                }
                else
                {
                    baseResponce.StatusCode = StatusCode.OK;
                }

                return baseResponce;
            }
            catch (Exception ex)
            {
                return new BaseResponce<Employee>()
                {
                    Description = $"[GetEmployeeByIdAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<IEnumerable<Employee>>> FindEmployeesAsync(Expression<Func<Employee, bool>> predicate)
        {
            var baseResponce = new BaseResponce<IEnumerable<Employee>>();

            try
            {
                var employees = await _employeeRepository.FindAsync(predicate);
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
                return new BaseResponce<IEnumerable<Employee>>()
                {
                    Description = $"[FindEmployeesAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> AddEmployeeAsync(Employee employee)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                await _employeeRepository.AddAsync(employee);
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
                    Description = $"[AddEmployeeAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> RemoveEmployeeByIdAsync(int id)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee != null)
                {
                    await _employeeRepository.RemoveAsync(employee);
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
                    Description = $"[RemoveEmployeeByIdAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponce<bool>> UpdateEmployeeAsync(Employee employee)
        {
            var baseResponce = new BaseResponce<bool>();

            try
            {
                await _employeeRepository.UpdateAsync(employee);
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
                    Description = $"[UpdateEmployeeAsync] : {ex.Message}"
                };
            }
        }
    }
}
