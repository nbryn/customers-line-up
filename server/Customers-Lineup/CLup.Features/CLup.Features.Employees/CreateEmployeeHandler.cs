using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;
using CLup.Features.Employees.Commands;
using CLup.Features.Extensions;

namespace CLup.Features.Employees
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly CLupContext _context;

        public CreateEmployeeHandler(CLupContext context, IMapper mapper) => _context = context;

        public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
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
                    .Finally(employee => _context.AddAndSave(employee));
        }
    }
}