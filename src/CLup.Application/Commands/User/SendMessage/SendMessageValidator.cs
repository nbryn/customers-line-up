using FluentValidation;

namespace CLup.Application.Commands.User.SendMessage
{
    public class SendUserMessageValidator : AbstractValidator<SendUserMessageCommand>
    {
        public SendUserMessageValidator()
        {
            RuleFor(message => message.SenderId).NotNull();
            RuleFor(b => b.ReceiverId).NotNull();
            RuleFor(b => b.Title).NotNull();
            RuleFor(b => b.Content).NotNull().Length(5, 150);
            RuleFor(b => b.Type).NotNull();
        }
    }
}