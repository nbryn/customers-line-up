using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.Business.Employee.Models
{
    public class FetchEmployeesQuery : IRequest<Result<List<EmployeeDto>>>
    {
        public string BusinessId { get; set; }
    }
}


