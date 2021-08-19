using System.Collections.Generic;

using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Employees.Queries
{
    public class FetchEmployeesQuery : IRequest<Result<List<EmployeeDTO>>>
    {
        public string BusinessId { get; set; }
    }
}


