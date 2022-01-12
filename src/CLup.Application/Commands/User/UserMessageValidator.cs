using CLup.Domain.Message;
using CLup.Domain.User;
using FluentValidation;

namespace CLup.Application.Commands.User
{
    public class UserMessageValidator : AbstractValidator<UserMessage>
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