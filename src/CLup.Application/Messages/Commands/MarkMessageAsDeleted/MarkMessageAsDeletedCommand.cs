using System;
using CLup.Application.Shared;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeleted;

public sealed class MarkMessageAsDeletedCommand : IRequest<Result>
{
    public UserId UserId { get; set; }

    public Guid SenderId { get; init; }

    public Guid ReceiverId { get; init; }

    public Guid MessageId { get; init; }

    public bool RequestMadeByBusiness { get; init; }

    public bool ForSender { get; init; }

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
