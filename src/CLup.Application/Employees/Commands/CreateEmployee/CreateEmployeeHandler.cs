using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Employees;
using CLup.Domain.Users;
using FluentValidation;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
{
    private readonly IValidator<Employee> _validator;
    private readonly ICLupRepository _repository;

    public CreateEmployeeHandler(IValidator<Employee> validator, ICLupRepository repository)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.OwnerId, command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFoundAsync(business => GetUser(business, command), UserErrors.NotFound)
            .FlatMap(entry => entry.Value.business.AddEmployee(entry.Value.user, entry.Value.employee),
                HttpCode.BadRequest)
            .AndThen(entry => entry.Value.employee)
            .Validate(_validator)
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));

    private async Task<(Business business, User user, Employee employee)?> GetUser(
        Business business,
        CreateEmployeeCommand command)
    {
        var user = await _repository.FetchUserAggregate(command.UserId);
        if (user == null)
        {
            return null;
        }

        return (business, user, command.MapToEmployee(user));
    }
}
