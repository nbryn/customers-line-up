using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.Business.Employee.Models
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

