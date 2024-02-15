using FluentValidation;

namespace CLup.API.Contracts.Messages.MarkMessageAsDeletedForBusiness;

public class MarkMessageAsDeletedForBusinessRequestValidator : AbstractValidator<MarkMessageAsDeletedForBusinessRequest>
{
    public MarkMessageAsDeletedForBusinessRequestValidator()
    {
        RuleFor(request => request.SenderId).NotEmpty();
        RuleFor(request => request.MessageId).NotEmpty();
        RuleFor(request => request.ReceivedMessage).NotEmpty();
    }
}
