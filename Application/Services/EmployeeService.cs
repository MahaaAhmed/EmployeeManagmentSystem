using Core.Models;
using Core.Repositories;
using EmployeeManagmentSystem.Sevice.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        
        public EmployeeService(IEmployeeRepository employeeRepository, IHubContext<NotificationHub> hubContext)
        {
            _employeeRepository = employeeRepository;
            _hubContext = hubContext;
            
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
             await _employeeRepository.AddAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
           return await _employeeRepository.GetAllAsync();
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return _employeeRepository.GetByIdAsync(id);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }
        public async Task NotifyEmployee(Employee employee)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Employee : {employee.Name}");
        }
        public async Task DeactivateInactiveEmployeesAsync()
        {
            await _employeeRepository.DeactivateEmployees();
        }
    }
}
