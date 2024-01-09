using System;
using CLup.Application.Shared;
using MediatR;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BusinessId { get; init; }

    public Guid UserId { get; init; }

    public string CompanyEmail { get; init; }
}
