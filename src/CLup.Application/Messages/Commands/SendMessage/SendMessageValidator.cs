using FluentValidation;

namespace CLup.Application.Messages.Commands.SendMessage;

public sealed class SendMessageValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageValidator()
    {
        RuleFor(command => command.SenderId).NotNull();
        RuleFor(command => command.ReceiverId).NotNull();
        RuleFor(command => command.Title).NotNull();
        RuleFor(command => command.Content).NotNull().Length(5, 150);
        RuleFor(command => command.Type).IsInEnum();
    }
}
