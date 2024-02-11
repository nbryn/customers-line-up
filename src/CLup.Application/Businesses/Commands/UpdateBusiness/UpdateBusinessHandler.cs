using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Application.Shared;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessHandler : IRequestHandler<UpdateBusinessCommand, Result>
{
    private readonly IValidator<Business> _businessValidator;
    private readonly ICLupRepository _repository;

    public UpdateBusinessHandler(IValidator<Business> businessValidator, ICLupRepository repository)
    {
        _businessValidator = businessValidator;
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.OwnerId, command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .AndThen(business => business.Update(command.BusinessData, command.Address, command.Coords, command.BusinessHours, command.Type))
            .Validate(_businessValidator)
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
