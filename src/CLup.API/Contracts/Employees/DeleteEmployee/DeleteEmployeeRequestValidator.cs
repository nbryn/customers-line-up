namespace CLup.API.Contracts.Employees.DeleteEmployee;

public sealed class DeleteEmployeeRequestValidator : AbstractValidator<DeleteEmployeeRequest>
{
    public DeleteEmployeeRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.EmployeeId).NotEmpty();
    }
}
