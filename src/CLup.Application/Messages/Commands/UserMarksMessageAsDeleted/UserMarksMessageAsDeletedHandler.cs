using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Users;
using MediatR;

namespace CLup.Application.Messages.Commands.UserMarksMessageAsDeleted;

public sealed class UserMarksMessageAsDeletedHandler : IRequestHandler<UserMarksMessageAsDeletedCommand, Result>
{
    private readonly ICLupRepository _repository;

    public UserMarksMessageAsDeletedHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UserMarksMessageAsDeletedCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.UserId)
            .FailureIfNotFound(UserErrors.NotFound)
            .FailureIfNotFound(user => user.GetMessageById(command.MessageId, command.ForSender), MessageErrors.NotFound)
            .AndThen(message => command.ForSender ? message?.DeletedBySender() : message?.DeletedByReceiver())
            .FinallyAsync(message => _repository.UpdateEntity(message.Id.Value, message, cancellationToken));
}
