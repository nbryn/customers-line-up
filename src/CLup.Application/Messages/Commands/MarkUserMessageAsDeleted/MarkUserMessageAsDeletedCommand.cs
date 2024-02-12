using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkUserMessageAsDeleted;

public sealed class MarkUserMessageAsDeletedCommand : IRequest<Result>
{
    public MessageId MessageId { get; }

    public UserId SenderId { get; }

    public BusinessId ReceiverId { get; }

    public bool ForSender { get; }

    public MarkUserMessageAsDeletedCommand(MessageId messageId, UserId senderId, BusinessId receiverId, bool forSender)
    {
        MessageId = messageId;
        SenderId = senderId;
        ReceiverId = receiverId;
        ForSender = forSender;
    }
}
