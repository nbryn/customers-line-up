using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public Guid UserId { get; set; }

    public string CompanyEmail { get; set; }
}
