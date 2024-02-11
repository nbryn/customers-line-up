using CLup.Domain.Messages.ValueObjects;
using FluentValidation;

namespace CLup.Domain.Messages.Validation;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator(
        IValidator<MessageData> messageDataValidator,
        IValidator<MessageMetadata> metadataValidator)
    {
        RuleFor(message => message.MessageData).SetValidator(messageDataValidator);
        RuleFor(message => message.Metadata).SetValidator(metadataValidator);
        RuleFor(message => message.Type).NotEmpty().IsInEnum();
        RuleFor(message => message.SenderId).NotNull();
        RuleFor(message => message.ReceiverId).NotNull();
    }
}
