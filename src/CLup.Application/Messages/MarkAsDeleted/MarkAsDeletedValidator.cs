using FluentValidation;

namespace CLup.Application.Messages.MarkAsDeleted
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