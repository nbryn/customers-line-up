using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using CLup.Context;
using CLup.Employees.DTO;
using CLup.Employees.Interfaces;
using CLup.Extensions;
using CLup.Util;

namespace CLup.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(
            CLupContext context,
            IMapper mapper)
            
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> CreateEmployee(NewEmployeeRequest request)
        {
            Employee newEmployee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                BusinessId = request.BusinessId,
                UserEmail = request.PrivateEmail,
                CompanyEmail = request.CompanyEmail,
            };

            _context.Employees.Add(newEmployee);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Created);
        }

        public async Task<ServiceResponse> DeleteEmployee(string email, string businessId)
        {
            Employee employee = await FindEmployeeByEmailAndBusiness(email, businessId);

            if (employee == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Deleted);
        }

        public async Task<Employee> FindEmployeeByEmailAndBusiness(string email, string businessId)
        {
            Employee employee = await _context.Employees.Include(e => e.Business)
                                                        .Include(e => e.User)
                                                        .FirstOrDefaultAsync(e => e.UserEmail == email &&
                                                                            e.BusinessId == businessId);

            return employee;
        }

          public async Task<ServiceResponse<EmployeeDTO>> FindEmployeeByEmail(string email)
        {
            Employee employee = await _context.Employees.Include(e => e.Business)
                                                        .Include(e => e.User)
                                                        .FirstOrDefaultAsync(e => e.UserEmail == email);

            return this.AssembleResponse<Employee, EmployeeDTO>(employee, _mapper);
        }

        public async Task<ServiceResponse<IList<EmployeeDTO>>> FindEmployeesByBusiness(string businessId)
        {
            IList<Employee> employees = await _context.Employees.Include(e => e.Business)
                                                                .Include(e => e.User)
                                                                .Where(e => e.BusinessId == businessId)
                                                                .ToListAsync();

            return this.AssembleResponse<Employee, EmployeeDTO>(employees, _mapper);
        }

    }
}