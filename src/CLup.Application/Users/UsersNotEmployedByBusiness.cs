using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users
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
            public IList<UserDto> Users { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.BusinessId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Result<Model>>
        {
            private readonly IReadOnlyDbContext _readContext;
            private readonly IMapper _mapper;

            public Handler(IReadOnlyDbContext readContext, IMapper mapper)
            {
                _readContext = readContext;
                _mapper = mapper;
            }

            public async Task<Result<Model>> Handle(Query query,
                CancellationToken cancellationToken)
            {
                var notAlreadyEmployedByBusiness = new List<UserDto>();

                foreach (var user in await _readContext.Users.ToListAsync())
                {
                    var employee = await _readContext.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id &&
                        e.BusinessId == query.BusinessId);
                    if (employee == null)
                    {
                        notAlreadyEmployedByBusiness.Add(_mapper.Map<UserDto>(user));
                    }
                }

                var result = new Model
                {
                    BusinessId = query.BusinessId,
                    Users = notAlreadyEmployedByBusiness,
                };

                return Result.Ok(result);
            }
        }
    }
}