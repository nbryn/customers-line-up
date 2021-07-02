using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Employees
{
    public class DeleteEmployee
    {
        public class Command : IRequest<Result>
        {
            public string BusinessId { get; set; }
            public string UserEmail { get; set; }

            public Command(string businessId, string userEmail)
            {
                businessId = BusinessId;
                UserEmail = UserEmail;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;
            public Handler(CLupContext context) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserEmail == command.UserEmail &&
                                                                            e.BusinessId == command.BusinessId);
                if (employee == null)
                {
                    return Result.NotFound();
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Result.Ok();
            }
        }
    }
}

