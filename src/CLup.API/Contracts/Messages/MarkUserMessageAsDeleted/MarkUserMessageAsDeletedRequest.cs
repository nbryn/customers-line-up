using CLup.Application.Messages.Commands.MarkUserMessageAsDeleted;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using MId = CLup.Domain.Messages.ValueObjects.MessageId;

namespace CLup.API.Contracts.Messages.MarkUserMessageAsDeleted;

public readonly record struct MarkUserMessageAsDeletedRequest(Guid MessageId, Guid ReceiverId, bool? ForSender)
{
    public MarkUserMessageAsDeletedCommand MapToCommand(UserId userId) =>
        new(MId.Create(MessageId), userId, BId.Create(ReceiverId), ForSender.Value);
}
