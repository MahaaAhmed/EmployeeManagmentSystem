using Core.Models;

using EmployeeManagmentSystem.Core.ViewModels;
using EmployeeManagmentSystem.Service.Services;
using EmployeeManagmentSystem.Sevice.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        
        private readonly IEmployeeService _employeeService;
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        public EmployeeController(IEmployeeService employeeService,AppDbContext context , IHubContext<NotificationHub> hubContext)
        {
            _employeeService = employeeService;
            _context = context;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            await _employeeService.UpdateEmployeeAsync(employee);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Employee Updated");
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Employee Deleted");
            return NoContent();
        }



        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            _employeeService.AddEmployeeAsync(employee);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Employee created");
            
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }



    }
}
