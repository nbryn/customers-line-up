using CLup.Application.Messages.Commands.SendBusinessMessage;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.API.Messages.Contracts.SendBusinessMessage;

public readonly record struct SendBusinessMessageRequest(
    Guid SenderId,
    Guid ReceiverId,
    string Title,
    string Content,
    MessageType Type)
{
    public SendBusinessMessageCommand MapToCommand(UserId userId) =>
        new(userId, BusinessId.Create(SenderId), UserId.Create(ReceiverId), new MessageData(Title, Content), Type);
}
