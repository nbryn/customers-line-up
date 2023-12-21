using System;

namespace CLup.Application.Messages;

public sealed class MessageDto
{
    public Guid Id { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Date { get; set; }

    public string Sender { get; set; }

    public string Receiver { get; set; }
}
