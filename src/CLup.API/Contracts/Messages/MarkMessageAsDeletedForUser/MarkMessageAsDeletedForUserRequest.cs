using CLup.Application.Messages.Commands.MarkMessageAsDeletedForUser;
using CLup.Domain.Users.ValueObjects;
using MId = CLup.Domain.Messages.ValueObjects.MessageId;

namespace CLup.API.Contracts.Messages.MarkMessageAsDeletedForUser;

public readonly record struct MarkMessageAsDeletedForUserRequest(Guid MessageId, bool? ReceivedMessage)
{
    public MarkMessageAsDeletedForUserCommand MapToCommand(UserId requesterId) =>
        new(MId.Create(MessageId), requesterId, ReceivedMessage.Value);
}
