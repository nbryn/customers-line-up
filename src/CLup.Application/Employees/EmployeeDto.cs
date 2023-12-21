using System;

namespace CLup.Application.Employees;

public sealed class EmployeeDto
{
    public Guid UserId { get; set; }

    public Guid BusinessId { get; set; }

    public string Name { get; set; }

    public string PrivateEmail { get; set; }


    public string CompanyEmail { get; set; }

    public string Business { get; set; }

    public string EmployedSince { get; set; }
}
