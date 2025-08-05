using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if ( employee is not null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();

        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeactivateEmployees()
        {

            var employees = await _context.Employees.Where(e => e.LastLoginDate < DateTime.Now.AddHours(-90) && e.IsActive).ToListAsync();

            foreach (var employee in employees)
            {
                employee.IsActive = false;
                _context.Update(employee);
            }

            await _context.SaveChangesAsync();

        }

    }
}
