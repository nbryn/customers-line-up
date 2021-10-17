using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Employees.Commands
{

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly CLupContext _context;

        public DeleteEmployeeHandler(CLupContext context) => _context = context;

        public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.UserId == command.UserId &&
                                                                     e.BusinessId == command.BusinessId)
                    .FailureIf("Employee not found.")
                    .Finally(employee => _context.RemoveAndSave(employee));
        }
    }
}