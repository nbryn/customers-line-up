using FluentValidation;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeleted;

public sealed class MarkMessageAsDeletedValidator : AbstractValidator<MarkMessageAsDeletedCommand>
{
    public MarkMessageAsDeletedValidator()
    {
        RuleFor(command => command.SenderId).NotNull();
        RuleFor(command => command.ReceiverId).NotNull();
        RuleFor(command => command.MessageId).NotNull();
        RuleFor(command => command.ForSender).NotNull();
        RuleFor(command => command.RequestMadeByBusiness).NotNull();
    }
}
