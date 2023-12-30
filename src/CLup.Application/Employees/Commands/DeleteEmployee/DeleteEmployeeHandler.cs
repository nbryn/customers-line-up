using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Employees.ValueObjects;
using MediatR;

namespace CLup.Application.Employees.Commands.DeleteEmployee;

public sealed class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Result>
{
    private readonly ICLupRepository _repository;

    public DeleteEmployeeHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIf(BusinessErrors.NotFound)
            .Ensure(business => business.OwnerId.Value == command.OwnerId.Value, HttpCode.Forbidden,
                BusinessErrors.InvalidOwner)
            .FailureIf(business => business.GetEmployeeById(EmployeeId.Create(command.UserId)), EmployeeErrors.NotFound)
            .Finally(async employee => await _repository.RemoveAndSave(employee));
}
