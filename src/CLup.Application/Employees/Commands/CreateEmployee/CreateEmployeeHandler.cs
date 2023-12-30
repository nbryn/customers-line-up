using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Employees;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;
using FluentValidation;
using MediatR;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
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
        => await _repository.FetchUserAggregate(command.OwnerId)
            .FailureIf(UserErrors.NotFound)
            .Ensure(user => user.Role != Role.Owner, HttpCode.BadRequest, EmployeeErrors.OwnerCannotBeEmployee)
            .AndThen(user => user.UpdateRole(Role.Employee))
            .AndThen(_ => _mapper.Map<Employee>(command))
            .Validate(_validator)
            .Finally(async employee => await _repository.AddAndSave(employee));
}
