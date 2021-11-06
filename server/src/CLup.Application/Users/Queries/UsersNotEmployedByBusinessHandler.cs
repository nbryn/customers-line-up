using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Users.Queries.Responses;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users.Queries
{
    public class UsersNotEmployedByBusinessHandler : IRequestHandler<UsersNotEmployedByBusinessQuery, Result<UsersNotEmployedByBusinessResponse>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UsersNotEmployedByBusinessHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UsersNotEmployedByBusinessResponse>> Handle(UsersNotEmployedByBusinessQuery query, CancellationToken cancellationToken)
        {
            var notAlreadyEmployedByBusiness = new List<UserDto>();

            foreach (var user in await _context.Users.ToListAsync())
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id &&
                                                                            e.BusinessId == query.BusinessId);
                if (employee == null)
                {
                    notAlreadyEmployedByBusiness.Add(_mapper.Map<UserDto>(user));
                }
            }

            var result = new UsersNotEmployedByBusinessResponse
            {
                BusinessId = query.BusinessId,
                Users = notAlreadyEmployedByBusiness,
            };

            return Result.Ok<UsersNotEmployedByBusinessResponse>(result);
        }
    }
}