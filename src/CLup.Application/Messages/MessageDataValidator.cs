using CLup.Domain.Messages;
using FluentValidation;

namespace CLup.Application.Shared.Messages
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