using FluentValidation;

namespace CLup.Application.Shared.Messages.Send
{
    public class SendMessageValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageValidator()
        {
            RuleFor(message => message.SenderId).NotNull();
            RuleFor(b => b.ReceiverId).NotNull();
            RuleFor(b => b.Title).NotNull();
            RuleFor(b => b.Content).NotNull().Length(5, 150);
            RuleFor(b => b.Type).NotNull();
        }
    }
}