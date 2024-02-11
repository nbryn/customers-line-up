using FluentValidation;

namespace CLup.API.Contracts.Messages.SendBusinessMessage;

public class SendBusinessMessageRequestValidator : AbstractValidator<SendBusinessMessageRequest>
{
    public SendBusinessMessageRequestValidator()
    {
        RuleFor(request => request.SenderId).NotNull();
        RuleFor(request => request.ReceiverId).NotNull();
        RuleFor(request => request.Title).NotNull();
        RuleFor(request => request.Content).NotNull().Length(5, 150);
        RuleFor(request => request.Type).NotEmpty().IsInEnum();
    }
}
