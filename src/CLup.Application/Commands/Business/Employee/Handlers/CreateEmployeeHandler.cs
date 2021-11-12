using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Commands.Business.Employee.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.Business.Employee.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IValidator<Domain.Business.Employee.Employee> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            IValidator<Domain.Business.Employee.Employee> validator,
            ICLupDbContext context, 
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
                    .AndThen(() => _mapper.Map<Domain.Business.Employee.Employee>(command))
                    .Validate(_validator)
                    .Finally(employee => _context.AddAndSave(employee));
        }
    }
}