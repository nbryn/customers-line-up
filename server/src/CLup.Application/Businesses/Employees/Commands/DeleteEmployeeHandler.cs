using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Extensions;
using CLup.Application.Shared;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.Employees.Commands
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