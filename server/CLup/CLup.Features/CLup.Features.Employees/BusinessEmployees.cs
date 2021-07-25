using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Employees
{
    public class BusinessEmployees
    {
        public class Query : IRequest<Result<List<EmployeeDTO>>>
        {
            public string BusinessId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<EmployeeDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<EmployeeDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {

                return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                        .FailureIfDiscard("Business not found")
                        .AndThen(() => _context.Employees
                                    .Include(e => e.Business)
                                    .Include(e => e.User)
                                    .Where(e => e.BusinessId == query.BusinessId))

                        .AndThen(employees => _mapper.ProjectTo<EmployeeDTO>(employees).ToListAsync());
            }
        }
    }
}

