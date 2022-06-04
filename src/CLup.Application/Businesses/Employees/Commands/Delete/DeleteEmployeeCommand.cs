using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }

        public string BusinessId { get; set; }

        public string UserId { get; set; }

        public DeleteEmployeeCommand(string ownerEmail, string businessId, string userId)
        {
            OwnerEmail = ownerEmail;
            BusinessId = businessId;
            UserId = userId;
        }
    }
}