using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Users
{
    public class UsersNotEmployedByBusiness
    {
        public class Query : IRequest<Result<Model>>
        {
            public string BusinessId { get; set; }
        }

        public class Model
        {
            public string BusinessId { get; set; }
            public IList<UserDTO> Users { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Model>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Model>> Handle(Query query, CancellationToken cancellationToken)
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

                var result = new Model
                {
                    BusinessId = query.BusinessId,
                    Users = notAlreadyEmployedByBusiness,
                };

                return Result.Ok<Model>(result);
            }
        }
    }
}

