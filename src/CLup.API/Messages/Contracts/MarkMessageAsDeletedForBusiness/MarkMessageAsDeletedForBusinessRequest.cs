using CLup.Application.Messages.Commands.MarkMessageAsForBusinessDeleted;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using MId = CLup.Domain.Messages.ValueObjects.MessageId;

namespace CLup.API.Messages.Contracts.MarkMessageAsDeletedForBusiness;

public readonly record struct MarkMessageAsDeletedForBusinessRequest(Guid SenderId, Guid MessageId, bool? ReceivedMessage)
{
    public MarkMessageAsDeletedForBusinessCommand MapToCommand(UserId requesterId) =>
        new(requesterId,MId.Create(MessageId), BId.Create(SenderId), ReceivedMessage.Value);
}
