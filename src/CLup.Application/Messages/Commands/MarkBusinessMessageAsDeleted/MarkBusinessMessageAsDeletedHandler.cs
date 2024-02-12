using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Messages.Enums;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkBusinessMessageAsDeleted;

public class MarkBusinessMessageAsDeletedHandler : IRequestHandler<MarkBusinessMessageAsDeletedCommand, Result>
{
    private readonly ICLupRepository _repository;

    public MarkBusinessMessageAsDeletedHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(MarkBusinessMessageAsDeletedCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.RequesterId, command.SenderId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFoundAsync(async business => command.ForSender
                    ? business?.GetSentMessageById(command.MessageId)
                    : (await _repository.FetchUserAggregate(command.ReceiverId))?.GetReceivedMessageById(command.MessageId),
                MessageErrors.NotFound)
            .AndThen(message => message?.Sender.MarkMessageAsDeleted(message, command.ForSender))
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
