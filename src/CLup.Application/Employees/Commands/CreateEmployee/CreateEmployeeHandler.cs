using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Employees;
using CLup.Domain.Users.Enums;
using FluentValidation;
using MediatR;

namespace CLup.Application.Employees.Commands.CreateEmployee
{
    using Shared.Result;

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IValidator<Employee> _validator;
        private readonly ICLupRepository _repository;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(
            IValidator<Employee> validator,
            ICLupRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("Owner not found.")
                .Ensure(user => user.Role != Role.Owner, "Owner cannot be employee.", HttpCode.Conflict)
                .FailureIf(async _ => await _repository.FetchUserAggregate(command.UserId), "User not found.")
                .AndThen(user => user.UpdateRole(Role.Employee))
                .AndThen(_ => _mapper.Map<Employee>(command))
                .Validate(_validator)
                .Finally(employee => _repository.AddAndSave(employee));
    }
}