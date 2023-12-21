using System;
using MediatR;
using CLup.Application.Shared.Result;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public Guid UserId { get; set; }

    public string CompanyEmail { get; set; }
}
