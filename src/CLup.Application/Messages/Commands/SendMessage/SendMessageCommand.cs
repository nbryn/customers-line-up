using System;
using CLup.Application.Shared;
using CLup.Domain.Messages.Enums;
using MediatR;

namespace CLup.Application.Messages.Commands.SendMessage;

public sealed class SendMessageCommand : IRequest<Result>
{
    public Guid SenderId { get; init; }

    public Guid ReceiverId { get; init; }

    public string Title { get; init; }

    public string Content { get; init; }

    public MessageType Type { get; init; }

    public SendMessageCommand(
        Guid senderId,
        Guid receiverId,
        string title,
        string content,
        MessageType type)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Title = title;
        Content = content;
        Type = type;
    }
}
