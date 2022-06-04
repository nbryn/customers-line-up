using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses.Employees;
using FluentValidation;
using MediatR;

namespace CLup.Application.Businesses.Employees.Commands.Create
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IValidator<Employee> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            IValidator<Employee> validator,
            ICLupDbContext context,
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .Ensure(user => user.Role != Domain.Users.Role.Owner, (HttpCode.Conflict, "Owner cannot be employee."))
                .AndThenDiscard(user => user.UpdateRole(Domain.Users.Role.Employee))
                .AndThen(() => _mapper.Map<Employee>(command))
                .Validate(_validator)
                .Finally(employee => _context.AddAndSave(employee));
    }
}