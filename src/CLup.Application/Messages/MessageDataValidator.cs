using CLup.Domain.Messages;
using CLup.Domain.Messages.ValueObjects;
using FluentValidation;

namespace CLup.Application.Messages
{
    public class MessageDataValidator : AbstractValidator<MessageData>
    {
        public MessageDataValidator()
        {
            RuleFor(b => b.Title).NotNull();
            RuleFor(b => b.Content).NotNull().Length(5, 150);
        }
    }
}