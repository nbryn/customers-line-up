using CLup.Domain.Messages;
using FluentValidation;

namespace CLup.Application.Commands.User
{
    public class UserMessageValidator : AbstractValidator<Message>
    {
        public UserMessageValidator(
            IValidator<MessageData> messageDataValidator,
            IValidator<MessageMetadata> metadataValidator)
        {
            RuleFor(message => message.MessageData).SetValidator(messageDataValidator);
            RuleFor(message => message.Metadata).SetValidator(metadataValidator);
            RuleFor(message => message.Type).IsInEnum();
            RuleFor(message => message.SenderId).NotNull();
            RuleFor(message => message.ReceiverId).NotNull();
        }
    }
}