using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;

namespace CLup.Application.Businesses.Employees.Commands.Delete
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public DeleteEmployeeHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetEmployee(command.BusinessId, command.UserId),
                    "Employee or business not found.")
                .Finally(employee => _context.RemoveAndSave(employee));
    }
}