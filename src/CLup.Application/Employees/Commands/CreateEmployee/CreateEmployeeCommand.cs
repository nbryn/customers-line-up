using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Employees.Commands.CreateEmployee
{
    using Shared.Result;

    public class CreateEmployeeCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }

        public string BusinessId { get; set; }

        public string UserId { get; set; }

        public string CompanyEmail { get; set; }
    }
}