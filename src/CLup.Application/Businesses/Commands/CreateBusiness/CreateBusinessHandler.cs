using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;
using FluentValidation;

namespace CLup.Application.Businesses.Commands.CreateBusiness;

public sealed class CreateBusinessHandler : IRequestHandler<CreateBusinessCommand, Result>
{
    private readonly IValidator<Business> _validator;
    private readonly ICLupRepository _repository;

    public CreateBusinessHandler(IValidator<Business> validator, ICLupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.OwnerId)
            .FailureIfNotFound(UserErrors.NotFound)
            .AndThen(user => user.UpdateRole(Role.Owner))
            .AndThen(_ => command.MapToBusiness())
            .Validate(_validator)
            .FinallyAsync(business => _repository.AddAndSave(cancellationToken, business));
}
