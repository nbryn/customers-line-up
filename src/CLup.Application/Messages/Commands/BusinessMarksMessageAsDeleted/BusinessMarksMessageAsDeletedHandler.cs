using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Messages.Enums;
using MediatR;

namespace CLup.Application.Messages.Commands.BusinessMarksMessageAsDeleted;

public class BusinessMarksMessageAsDeletedHandler : IRequestHandler<BusinessMarksMessageAsDeletedCommand, Result>
{
    private readonly ICLupRepository _repository;

    public BusinessMarksMessageAsDeletedHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(BusinessMarksMessageAsDeletedCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.RequesterId, command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => business.GetMessageById(command.MessageId, command.ForSender), MessageErrors.NotFound)
            .AndThen(message => command.ForSender ? message?.DeletedBySender() : message?.DeletedByReceiver())
            .FinallyAsync(message => _repository.UpdateEntity(message.Id.Value, message, cancellationToken));
}
