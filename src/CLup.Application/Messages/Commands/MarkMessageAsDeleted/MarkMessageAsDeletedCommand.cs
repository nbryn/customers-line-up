using System;
using CLup.Application.Shared;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeleted;

public sealed class MarkMessageAsDeletedCommand : IRequest<Result>
{
    public UserId UserId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid MessageId { get; set; }

    public bool RequestMadeByBusiness { get; set; }

    public bool ForSender { get; set; }

    public MarkMessageAsDeletedCommand(
        UserId userId,
        Guid messageId,
        Guid senderId,
        bool requestMadeByBusiness,
        bool forSender)
    {
        UserId = userId;
        MessageId = messageId;
        SenderId = senderId;
        RequestMadeByBusiness = requestMadeByBusiness;
        ForSender = forSender;
    }
}
