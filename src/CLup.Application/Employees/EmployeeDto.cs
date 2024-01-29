using System;
using CLup.Domain.Employees;

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

    public EmployeeDto FromEmployee(Employee employee)
    {
        UserId = employee.UserId.Value;
        BusinessId = employee.BusinessId.Value;
        Name = employee.User.UserData.Name;
        PrivateEmail = employee.User.UserData.Email;
        CompanyEmail = employee.CompanyEmail;
        Business = employee.Business.BusinessData.Name;
        EmployedSince = employee.CreatedAt.ToString("dd/MM/yyyy");

        return this;
    }
}
