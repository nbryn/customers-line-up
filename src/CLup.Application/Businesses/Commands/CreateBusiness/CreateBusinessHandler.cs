using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using CLup.Application.Shared.Result;

namespace CLup.Application.Businesses.Commands.CreateBusiness;

public sealed class CreateBusinessHandler : IRequestHandler<CreateBusinessCommand, Result>
{
    private readonly IValidator<Business> _validator;
    private readonly ICLupRepository _repository;
    private readonly IMapper _mapper;

    public CreateBusinessHandler(
        IValidator<Business> validator,
        ICLupRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(UserId.Create(command.OwnerId))
            .FailureIf(UserErrors.NotFound(UserId.Create(command.OwnerId)))
            .AndThen(user => user.UpdateRole(Role.Owner))
            .AndThen(_ => _mapper.Map<Business>(command))
            .Validate(_validator)
            .Finally(business => _repository.AddAndSave(business));
}
