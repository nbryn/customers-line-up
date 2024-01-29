using System;
using CLup.Domain.Messages;

namespace CLup.Application.Messages;

public sealed class MessageDto
{
    public Guid Id { get; private set; }

    public Guid SenderId { get; private set; }

    public Guid ReceiverId { get; private set; }

    public string Title { get; private set; }

    public string Content { get; private set; }

    public string Date { get; private set; }

    public MessageDto FromMessage(Message message)
    {
        Id = message.Id.Value;
        SenderId = message.SenderId.Value;
        ReceiverId = message.ReceiverId.Value;
        Title = message.MessageData.Title;
        Content = message.MessageData.Content;
        Date = message.CreatedAt.ToString("MM/dd/yyyy");

        return this;
    }
}
