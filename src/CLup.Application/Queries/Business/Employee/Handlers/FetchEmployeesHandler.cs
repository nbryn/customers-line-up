using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.Business.Employee.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.Employee.Handlers
{

    public class FetchEmployeesHandler : IRequestHandler<FetchEmployeesQuery, Result<List<EmployeeDto>>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public FetchEmployeesHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeeDto>>> Handle(FetchEmployeesQuery query, CancellationToken cancellationToken)
        {

            return await _readOnlyContext.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .FailureIfDiscard("Business not found")
                    .AndThen(() => _readOnlyContext.Employees
                                .Include(e => e.Business)
                                .Include(e => e.User)
                                .Where(e => e.BusinessId == query.BusinessId))

                    .AndThen(employees => _mapper.ProjectTo<EmployeeDto>(employees).ToListAsync());
        }
    }
}