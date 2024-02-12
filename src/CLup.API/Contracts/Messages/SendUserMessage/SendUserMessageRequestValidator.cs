using FluentValidation;

namespace CLup.API.Contracts.Messages.SendUserMessage;

public sealed class SendUserMessageRequestValidator : AbstractValidator<SendUserMessageRequest>
{
    public SendUserMessageRequestValidator()
    {
        RuleFor(request => request.ReceiverId).NotEmpty();
        RuleFor(request => request.Title).NotEmpty();
        RuleFor(request => request.Content).NotEmpty().Length(5, 150);
        RuleFor(request => request.Type).NotEmpty().IsInEnum();
    }
}
