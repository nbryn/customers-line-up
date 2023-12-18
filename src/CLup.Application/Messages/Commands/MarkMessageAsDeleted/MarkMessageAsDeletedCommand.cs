using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeleted
{
    public class MarkMessageAsDeletedCommand : IRequest<Result>
    {
        public string UserEmail { get; set; }
        
        public string MessageId { get; set; }

        public bool ForSender { get; set; }

        public MarkMessageAsDeletedCommand()
        {
        }

        public MarkMessageAsDeletedCommand(
            string messageId,
            bool forSender)
        {
            MessageId = messageId;
            ForSender = forSender;
        }
    }
}