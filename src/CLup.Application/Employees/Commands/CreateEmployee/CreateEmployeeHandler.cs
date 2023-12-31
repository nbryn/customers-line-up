using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
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
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => GetUser(business, command), UserErrors.NotFound)
            .Ensure(entry => entry.Value.business.AddEmployee(entry.Value.user, entry.Value.employee).Success,
                HttpCode.BadRequest)
            .AndThen(entry => entry.Value.employee)
            .Validate(_validator)
            .Finally(_ => _repository.SaveChangesAsync(cancellationToken));

    private async Task<(Business business, User user, Employee employee)?> GetUser(
        Business business,
        CreateEmployeeCommand command)
    {
        var user = await _repository.FetchUserAggregate(UserId.Create(command.UserId));
        if (user == null)
        {
            return null;
        }

        return (business, user, _mapper.Map<Employee>(command));
    }
}
