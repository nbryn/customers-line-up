namespace CLup.API.Messages.Contracts.MarkMessageAsDeletedForBusiness;

public class MarkMessageAsDeletedForBusinessRequestValidator : AbstractValidator<MarkMessageAsDeletedForBusinessRequest>
{
    public MarkMessageAsDeletedForBusinessRequestValidator()
    {
        RuleFor(request => request.SenderId).NotEmpty();
        RuleFor(request => request.MessageId).NotEmpty();
        RuleFor(request => request.ReceivedMessage).NotEmpty();
    }
}
