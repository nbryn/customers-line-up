using CLup.Application.Messages.Commands.UserMarksMessageAsDeleted;
using CLup.Domain.Users.ValueObjects;
using MId = CLup.Domain.Messages.ValueObjects.MessageId;

namespace CLup.API.Contracts.Messages.UserMarksMessageAsDeleted;

public readonly record struct UserMarksMessageAsDeletedRequest(Guid MessageId, bool ForSender)
{
    public UserMarksMessageAsDeletedCommand MapToCommand(UserId userId) =>
        new(userId, MId.Create(MessageId), ForSender);
}
