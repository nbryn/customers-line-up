using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.BusinessMarksMessageAsDeleted;

public sealed class BusinessMarksMessageAsDeletedCommand : IRequest<Result>
{
    public UserId RequesterId { get; }

    public BusinessId BusinessId { get; }

    public MessageId MessageId { get; }

    public bool ForSender { get; }

    public BusinessMarksMessageAsDeletedCommand(
        UserId requesterId,
        BusinessId businessId,
        MessageId messageId,
        bool forSender)
    {
        RequesterId = requesterId;
        BusinessId = businessId;
        MessageId = messageId;
        ForSender = forSender;
    }
}
