using CLup.Domain.Employees;

namespace CLup.Application.Employees;

public sealed class EmployeeDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public Guid BusinessId { get; init; }

    public string Name { get; init; }

    public string PrivateEmail { get; init; }

    public string CompanyEmail { get; init; }

    public string Business { get; init; }

    public string EmployedSince { get; init; }

    public static EmployeeDto FromEmployee(Employee employee)
    {
        return new EmployeeDto()
        {
            Id = employee.Id.Value,
            UserId = employee.UserId.Value,
            BusinessId = employee.BusinessId.Value,
            Name = employee.User?.UserData.Name ?? "",
            PrivateEmail = employee.User?.UserData.Email ?? "",
            CompanyEmail = employee.CompanyEmail,
            Business = employee.Business?.BusinessData.Name ?? "",
            EmployedSince = employee.CreatedAt.ToString("dd/MM/yyyy"),
        };
    }
}
