using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Context;
using CLup.Util;

namespace CLup.Users
{
    public class UsersNotEmployedByBusiness
    {
        public class Query : IRequest<Result<IList<UserDTO>>>
        {
            public string BusinessId { get; set; }
            
            public Query(string businessId) => BusinessId = businessId;
        }
        public class Handler : IRequestHandler<Query, Result<IList<UserDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<IList<UserDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var notAlreadyEmployedByBusiness = new List<UserDTO>();

                foreach (var user in _context.Users)
                {
                    var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserEmail == user.Email &&
                                                                                e.BusinessId == query.BusinessId);
                    if (employee == null)
                    {
                        notAlreadyEmployedByBusiness.Add(_mapper.Map<UserDTO>(user));
                    }
                }

                return Result.Ok<IList<UserDTO>>(notAlreadyEmployedByBusiness);
            }
        }
    }
}

