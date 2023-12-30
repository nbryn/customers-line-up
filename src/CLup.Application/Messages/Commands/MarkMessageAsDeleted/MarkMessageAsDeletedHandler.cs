using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeleted;

public sealed class MarkMessageAsDeletedHandler : IRequestHandler<MarkMessageAsDeletedCommand, Result>
{
    private readonly ICLupRepository _repository;

    public MarkMessageAsDeletedHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(MarkMessageAsDeletedCommand command, CancellationToken cancellationToken)
        => await _repository.FetchMessage(MessageId.Create(command.MessageId), command.RequestMadeByBusiness)
            .FailureIf(MessageErrors.NotFound)
            .Ensure(async message => await Validate(message, command), HttpCode.Forbidden, MessageErrors.NoAccess)
            .AndThen(message => command.ForSender ? message?.DeletedBySender() : message?.DeletedByReceiver())
            .Finally(message => _repository.UpdateEntity(message.Id.Value, message));

    private async Task<bool> Validate(Message message, MarkMessageAsDeletedCommand command)
    {
        if (message.SenderId.Value != command.SenderId || message.ReceiverId.Value != command.ReceiverId)
        {
            return false;
        }

        var id = command.ForSender ? command.SenderId : command.ReceiverId;
        if (command.RequestMadeByBusiness)
        {
            var business = await _repository.FetchBusinessAggregate(BusinessId.Create(id));
            return business?.OwnerId.Value == id;
        }

        var user = await _repository.FetchUserAggregate(UserId.Create(id));
        return user?.Id.Value == id;
    }
}
