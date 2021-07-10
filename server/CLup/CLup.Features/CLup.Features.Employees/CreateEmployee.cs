using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;

namespace CLup.Features.Employees
{
    public class CreateEmployee
    {
        public class Command : IRequest<Result>
        {
            public string BusinessId { get; set; }

            public string PrivateEmail { get; set; }

            public string CompanyEmail { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.BusinessId).NotEmpty();
                RuleFor(x => x.PrivateEmail).EmailAddress();
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;

            public Handler(CLupContext context, IMapper mapper) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.PrivateEmail);

                var business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == command.BusinessId);

                if (user == null || business == null)
                {
                    return Result.NotFound();
                }

                var employee = new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    BusinessId = command.BusinessId,
                    UserEmail = command.PrivateEmail,
                    CompanyEmail = command.CompanyEmail,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return Result.Ok();
            }
        }
    }
}

