using CLup.Application.Messages.Commands.SendUserMessage;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.API.Messages.Contracts.SendUserMessage;

public readonly record struct SendUserMessageRequest(
    Guid ReceiverId,
    string Title,
    string Content,
    MessageType Type)
{
    public SendUserMessageCommand MapToCommand(UserId userId) =>
        new(userId, BusinessId.Create(ReceiverId), new MessageData(Title, Content), Type);
}
