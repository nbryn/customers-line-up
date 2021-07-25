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
using CLup.Features.Extensions;

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
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == command.PrivateEmail)
                        .FailureIfDiscard("User not found.")
                        .FailureIfDiscard(() => _context.Businesses.FirstOrDefaultAsync(b => b.Id == command.BusinessId), "Business not found.")
                        .AndThen(() => new Employee
                        {
                            Id = Guid.NewGuid().ToString(),
                            BusinessId = command.BusinessId,
                            UserEmail = command.PrivateEmail,
                            CompanyEmail = command.CompanyEmail,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,

                        })
                        .Execute(employee => _context.AddAndSave(employee));
            }
        }
    }
}

