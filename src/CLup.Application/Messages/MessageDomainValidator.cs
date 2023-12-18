using CLup.Domain.Messages;
using CLup.Domain.Messages.ValueObjects;
using FluentValidation;

namespace CLup.Application.Messages
{
    public class MessageDomainValidator : AbstractValidator<Message>
    {
        public MessageDomainValidator(
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