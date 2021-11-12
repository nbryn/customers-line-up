using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Commands.Business.Employee.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.Business.Employee.Handlers
{

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public DeleteEmployeeHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.UserId == command.UserId &&
                                                                e.BusinessId == command.BusinessId)
                    .FailureIf("Employee not found.")
                    .Finally(employee => _context.RemoveAndSave(employee));
        }
    }
}