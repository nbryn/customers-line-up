using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.Business.Employee.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using CLup.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.Employee.Handlers
{

    public class FetchEmployeesHandler : IRequestHandler<FetchEmployeesQuery, Result<List<EmployeeDto>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public FetchEmployeesHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeeDto>>> Handle(FetchEmployeesQuery query, CancellationToken cancellationToken)
        {

            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .FailureIfDiscard("Business not found")
                    .AndThen(() => _context.Employees
                                .Include(e => e.Business)
                                .Include(e => e.User)
                                .Where(e => e.BusinessId == query.BusinessId))

                    .AndThen(employees => _mapper.ProjectTo<EmployeeDto>(employees).ToListAsync());
        }
    }
}