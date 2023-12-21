using System;
using CLup.Application.Shared.Result;
using CLup.Domain.Messages.Enums;
using MediatR;

namespace CLup.Application.Messages.Commands.SendMessage;

public sealed class SendMessageCommand : IRequest<Result>
{
    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public MessageType Type { get; set; }

    public SendMessageCommand()
    {

    }

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
