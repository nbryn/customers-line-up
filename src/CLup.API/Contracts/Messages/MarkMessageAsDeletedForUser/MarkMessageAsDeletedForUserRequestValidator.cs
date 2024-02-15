using FluentValidation;

namespace CLup.API.Contracts.Messages.MarkMessageAsDeletedForUser;

public sealed class MarkMessageAsDeletedForUserRequestValidator : AbstractValidator<MarkMessageAsDeletedForUserRequest>
{
    public MarkMessageAsDeletedForUserRequestValidator()
    {
        RuleFor(request => request.MessageId).NotEmpty();
        RuleFor(request => request.ReceivedMessage).NotEmpty();
    }
}
