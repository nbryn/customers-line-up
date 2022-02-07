using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand : IRequest<Result>
    {

        public string BusinessId { get; set; }
        public string UserId { get; set; }

        public DeleteEmployeeCommand(string businessId, string userId)
        {
            BusinessId = businessId;
            UserId = userId;
        }

    }
}

