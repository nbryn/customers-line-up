using CLup.Application.Messages.Commands.MarkBusinessMessageAsDeleted;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using MId = CLup.Domain.Messages.ValueObjects.MessageId;
using UId = CLup.Domain.Users.ValueObjects.UserId;

namespace CLup.API.Contracts.Messages.MarkBusinessMessageAsDeleted;

public readonly record struct MarkBusinessMessageAsDeletedRequest(Guid SenderId, Guid ReceiverId, Guid MessageId, bool? ForSender)
{
    public MarkBusinessMessageAsDeletedCommand MapToCommand(UId requesterId) =>
        new(requesterId,MId.Create(MessageId), BId.Create(SenderId), UId.Create(ReceiverId), ForSender.Value);
}
