using FluentValidation;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeleted
{
    public class MarkMessageAsDeletedValidator : AbstractValidator<MarkMessageAsDeletedCommand>
    {
        public MarkMessageAsDeletedValidator()
        {
            RuleFor(message => message.MessageId).NotNull();
            RuleFor(b => b.ForSender).NotNull();
        }
    }
}