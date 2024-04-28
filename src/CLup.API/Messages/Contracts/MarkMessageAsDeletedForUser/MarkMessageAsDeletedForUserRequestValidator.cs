namespace CLup.API.Messages.Contracts.MarkMessageAsDeletedForUser;

public sealed class MarkMessageAsDeletedForUserRequestValidator : AbstractValidator<MarkMessageAsDeletedForUserRequest>
{
    public MarkMessageAsDeletedForUserRequestValidator()
    {
        RuleFor(request => request.MessageId).NotEmpty();
        RuleFor(request => request.ReceivedMessage).NotEmpty();
    }
}
