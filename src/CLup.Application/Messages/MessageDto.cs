using System;
using CLup.Domain.Messages;

namespace CLup.Application.Messages;

public sealed class MessageDto
{
    public Guid Id { get; init; }

    public Guid SenderId { get; init; }

    public Guid ReceiverId { get; init; }

    public string Title { get; init; }

    public string Content { get; init; }

    public string Date { get; init; }

    public static MessageDto FromMessage(Message message)
    {
        return new MessageDto()
        {
            Id = message.Id.Value,
            SenderId = message.SenderId.Value,
            ReceiverId = message.ReceiverId.Value,
            Title = message.MessageData.Title,
            Content = message.MessageData.Content,
            Date = message.CreatedAt.ToString("MM/dd/yyyy"),
        };
    }
}
