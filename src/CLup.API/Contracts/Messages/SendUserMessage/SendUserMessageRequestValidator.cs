using FluentValidation;

namespace CLup.API.Contracts.Messages.SendUserMessage;

public sealed class SendUserMessageRequestValidator : AbstractValidator<SendUserMessageRequest>
{
    public SendUserMessageRequestValidator()
    {
        RuleFor(request => request.ReceiverId).NotNull();
        RuleFor(request => request.Title).NotNull();
        RuleFor(request => request.Content).NotNull().Length(5, 150);
        RuleFor(request => request.Type).NotEmpty().IsInEnum();
    }
}
