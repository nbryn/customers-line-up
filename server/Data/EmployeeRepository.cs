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
            return Response.BadRequest;
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