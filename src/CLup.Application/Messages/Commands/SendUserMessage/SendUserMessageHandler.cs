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

namespace CLup.Application.Messages.Commands.SendUserMessage;

public sealed class SendUserMessageHandler : IRequestHandler<SendUserMessageCommand, Result>
{
    private readonly IValidator<Message> _validator;
    private readonly ICLupRepository _repository;

    public SendUserMessageHandler(
        IValidator<Message> validator,
        ICLupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result> Handle(SendUserMessageCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.SenderId)
            .FailureIfNotFound(UserErrors.NotFound)
            .FailureIfNotFoundAsync(_ => _repository.FetchBusinessById(command.ReceiverId), BusinessErrors.NotFound)
            .AndThen(_ => command.MapToUserMessage())
            .Validate(_validator)
            .FinallyAsync(message => _repository.AddAndSave(cancellationToken, message));
}
