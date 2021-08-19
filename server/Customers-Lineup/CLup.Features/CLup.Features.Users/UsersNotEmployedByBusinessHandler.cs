using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;
using CLup.Features.Users.Responses;
using CLup.Features.Users.Queries;

namespace CLup.Features.Users
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
            var notAlreadyEmployedByBusiness = new List<UserDTO>();

            foreach (var user in await _context.Users.ToListAsync())
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserEmail == user.Email &&
                                                                            e.BusinessId == query.BusinessId);
                if (employee == null)
                {
                    notAlreadyEmployedByBusiness.Add(_mapper.Map<UserDTO>(user));
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