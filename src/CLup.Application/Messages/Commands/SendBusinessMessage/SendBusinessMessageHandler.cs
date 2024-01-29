using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Messages;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;

namespace CLup.Application.Messages.Commands.SendBusinessMessage;

public sealed class SendBusinessMessageHandler : IRequestHandler<SendBusinessMessageCommand, Result>
{
    private readonly IValidator<Message> _validator;
    private readonly ICLupRepository _repository;

    public SendBusinessMessageHandler(
        IValidator<Message> validator,
        ICLupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result> Handle(SendBusinessMessageCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.RequesterId, command.SenderId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFoundAsync(_ => _repository.FetchUserAggregate(command.ReceiverId), UserErrors.NotFound)
            .AndThen(_ => command.MapToBusinessMessage())
            .Validate(_validator)
            .FinallyAsync(message => _repository.AddAndSave(cancellationToken, message));
}
