using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest<Result>
    {

        public string BusinessId { get; set; }
        public string UserId { get; set; }

        public DeleteEmployeeCommand(string businessId, string userId)
        {
            businessId = BusinessId;
            UserId = UserId;
        }

    }
}

