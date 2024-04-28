namespace CLup.API.Employees.Contracts.DeleteEmployee;

public sealed class DeleteEmployeeRequestValidator : AbstractValidator<DeleteEmployeeRequest>
{
    public DeleteEmployeeRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.EmployeeId).NotEmpty();
    }
}
