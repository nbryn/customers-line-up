using CLup.Application.Shared;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.UserMarksMessageAsDeleted;

public sealed class UserMarksMessageAsDeletedCommand : IRequest<Result>
{
    public UserId UserId { get; }

    public MessageId MessageId { get; }

    public bool ForSender { get; }

    public UserMarksMessageAsDeletedCommand(UserId userId, MessageId messageId, bool forSender)
    {
        UserId = userId;
        MessageId = messageId;
        ForSender = forSender;
    }
}
