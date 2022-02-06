using FluentValidation;

namespace CLup.Application.Commands.Shared.Message.MarkAsDeleted
{
    public class MarkAsDeletedValidator : AbstractValidator<MarkMessageAsDeletedCommand>
    {
        public MarkAsDeletedValidator()
        {
            RuleFor(message => message.MessageId).NotNull();
            RuleFor(b => b.ForSender).NotNull();
        }
    }
}