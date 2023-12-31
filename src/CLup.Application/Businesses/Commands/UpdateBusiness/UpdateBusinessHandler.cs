using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Application.Shared;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessHandler : IRequestHandler<UpdateBusinessCommand, Result>
{
    private readonly IValidator<Business> _businessValidator;
    private readonly ICLupRepository _repository;
    private readonly IMapper _mapper;

    public UpdateBusinessHandler(
        IValidator<Business> businessValidator,
        ICLupRepository repository,
        IMapper mapper)
    {
        _businessValidator = businessValidator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIfNotFound(BusinessErrors.NotFound)
            .Ensure(business => business.OwnerId.Value == command.OwnerId.Value, HttpCode.Forbidden,
                BusinessErrors.InvalidOwner)
            .AndThen(_ => _mapper.Map<Business>(command))
            .Validate(_businessValidator)
            .Finally(async updatedBusiness =>
                await _repository.UpdateEntity(command.BusinessId, updatedBusiness));
}
