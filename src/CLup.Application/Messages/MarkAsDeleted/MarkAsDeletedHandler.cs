using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;

namespace CLup.Application.Messages.MarkAsDeleted
{
    public class MarkAsDeletedHandler : IRequestHandler<MarkMessageAsDeletedCommand, Result>
    {
        private readonly ICLupRepository _repository;

        public MarkAsDeletedHandler(ICLupRepository repository) => _repository = repository;

        public async Task<Result> Handle(MarkMessageAsDeletedCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.UserEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetMessage(command.MessageId) ,"Message not found.")
                .AndThen(message => command.ForSender ? message.DeletedBySender() : message.DeletedByReceiver())
                .Finally(message => _repository.UpdateEntity(message.Id, message));
    }
}