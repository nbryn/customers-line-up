using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.Employees.Queries
{
    public class FetchEmployeesQuery : IRequest<Result<List<EmployeeDto>>>
    {
        public string BusinessId { get; set; }
    }
}


