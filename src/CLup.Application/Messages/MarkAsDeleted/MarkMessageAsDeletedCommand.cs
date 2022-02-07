using MediatR;

namespace CLup.Application.Shared.Messages.MarkAsDeleted
{
    public class MarkMessageAsDeletedCommand : IRequest<Result>
    {
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