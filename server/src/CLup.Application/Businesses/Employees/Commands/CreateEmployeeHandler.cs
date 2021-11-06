using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Extensions;
using CLup.Application.Shared;
using CLup.Data;
using CLup.Domain.Businesses.Employees;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.Employees.Commands
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IValidator<Employee> _validator;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            IValidator<Employee> validator,
            CLupContext context, 
            IMapper mapper) 
        { 
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId)
                    .FailureIfDiscard("User not found.")
                    .FailureIfDiscard(() => _context.Businesses.FirstOrDefaultAsync(b => b.Id == command.BusinessId), "Business not found.")
                    .AndThen(() => _mapper.Map<Employee>(command))
                    .Validate(_validator)
                    .Finally(employee => _context.AddAndSave(employee));
        }
    }
}