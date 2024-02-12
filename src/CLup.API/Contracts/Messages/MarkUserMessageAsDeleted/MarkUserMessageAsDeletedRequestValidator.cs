using FluentValidation;

namespace CLup.API.Contracts.Messages.MarkUserMessageAsDeleted;

public sealed class MarkUserMessageAsDeletedRequestValidator : AbstractValidator<MarkUserMessageAsDeletedRequest>
{
    public MarkUserMessageAsDeletedRequestValidator()
    {
        RuleFor(request => request.MessageId).NotEmpty();
        RuleFor(request => request.ReceiverId).NotEmpty();
        RuleFor(request => request.ForSender).NotEmpty();
    }
}
