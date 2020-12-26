using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using Logic.Context;
using Logic.DTO;
using Logic.Employees;

namespace Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ICLupContext _context;

        public EmployeeRepository(ICLupContext context)
        {
            _context = context;
        }
        public async Task<Response> CreateEmployee(NewEmployeeDTO request)
        {
            Employee newEmployee = new Employee
            {
                BusinessId = request.BusinessId,
                UserEmail = request.PrivateEmail,
                CompanyEmail = request.CompanyEmail,
            };

            _context.Employees.Add(newEmployee);

            await _context.SaveChangesAsync();

            return Response.Created;
        }

        public async Task<Response> DeleteEmployee(string email, int businessId)
        {
            Employee employee = await FindEmployeeByEmailAndBusiness(email, businessId);

            if (employee == null)
            {
                return Response.NotFound;
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return Response.Deleted;
        }

        public async Task<Employee> FindEmployeeByEmailAndBusiness(string email, int businessId)
        {
            Employee employee = await _context.Employees.Include(e => e.Business)
                                                        .Include(e => e.User)
                                                        .FirstOrDefaultAsync(e => e.UserEmail == email &&
                                                                            e.BusinessId == businessId);

            return employee;
        }

          public async Task<Employee> FindEmployeeByEmail(string email)
        {
            Employee employee = await _context.Employees.Include(e => e.Business)
                                                        .Include(e => e.User)
                                                        .FirstOrDefaultAsync(e => e.UserEmail == email);

            return employee;
        }

        public async Task<IList<Employee>> FindEmployeesByBusiness(int businessId)
        {
            IList<Employee> employees = await _context.Employees.Include(e => e.Business)
                                                                .Include(e => e.User)
                                                                .Where(e => e.BusinessId == businessId)
                                                                .ToListAsync();

            return employees;
        }

    }
}