using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;

namespace CLup.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly ICLupRepository _repository;

        public DeleteEmployeeHandler(ICLupRepository repository) => _repository = repository;

        public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetEmployee(command.BusinessId, command.UserId),
                    "Employee or business not found.")
                .Finally(employee => _repository.RemoveAndSave(employee));
    }
}