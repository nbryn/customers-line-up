using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest<Result>
    {

        public string BusinessId { get; set; }
        public string UserEmail { get; set; }

        public DeleteEmployeeCommand(string businessId, string userEmail)
        {
            businessId = BusinessId;
            UserEmail = UserEmail;
        }

    }
}

