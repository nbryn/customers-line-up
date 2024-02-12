using FluentValidation;

namespace CLup.API.Contracts.Messages.MarkBusinessMessageAsDeleted;

public class MarkBusinessMessageAsDeletedRequestValidator : AbstractValidator<MarkBusinessMessageAsDeletedRequest>
{
    public MarkBusinessMessageAsDeletedRequestValidator()
    {
        RuleFor(request => request.SenderId).NotEmpty();
        RuleFor(request => request.ReceiverId).NotEmpty();
        RuleFor(request => request.MessageId).NotEmpty();
        RuleFor(request => request.ForSender).NotEmpty();
    }
}
