using CLup.Application.Messages.Commands.BusinessMarksMessageAsDeleted;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using MId = CLup.Domain.Messages.ValueObjects.MessageId;

namespace CLup.API.Contracts.Messages.BusinessMarksMessageAsDeleted;

public readonly record struct BusinessMarksMessageAsDeletedRequest(Guid BusinessId, Guid MessageId, bool ForSender)
{
    public BusinessMarksMessageAsDeletedCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), MId.Create(MessageId), ForSender);
}
