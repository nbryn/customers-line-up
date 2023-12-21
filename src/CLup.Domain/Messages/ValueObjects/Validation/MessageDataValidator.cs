using FluentValidation;

namespace CLup.Domain.Messages.ValueObjects.Validation;

public class MessageDataValidator : AbstractValidator<MessageData>
{
    public MessageDataValidator()
    {
        RuleFor(b => b.Title).NotNull();
        RuleFor(b => b.Content).NotNull().Length(5, 150);
    }
}
