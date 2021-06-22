using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Context;
using CLup.Employees.DTO;
using CLup.Util;

namespace CLup.Employees
{
    public class BusinessEmployees
    {
        public class Query : IRequest<Result<IList<EmployeeDTO>>>
        {
            public string BusinessId { get; set; }

            public Query(string businessId) => BusinessId = businessId;
        }

        public class Handler : IRequestHandler<Query, Result<IList<EmployeeDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<IList<EmployeeDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var business = _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId);

                if (business == null)
                {
                    return Result.NotFound<IList<EmployeeDTO>>();
                }

                var employees = _context.Employees
                                    .Include(e => e.Business)
                                    .Include(e => e.User)
                                    .Where(e => e.BusinessId == query.BusinessId);

                var result = await _mapper.ProjectTo<EmployeeDTO>(employees).ToListAsync();

                return Result.Ok<IList<EmployeeDTO>>(result);
            }
        }
    }
}

