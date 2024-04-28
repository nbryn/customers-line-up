namespace CLup.API.Messages.Contracts.SendBusinessMessage;

public class SendBusinessMessageRequestValidator : AbstractValidator<SendBusinessMessageRequest>
{
    public SendBusinessMessageRequestValidator()
    {
        RuleFor(request => request.SenderId).NotEmpty();
        RuleFor(request => request.ReceiverId).NotEmpty();
        RuleFor(request => request.Title).NotEmpty();
        RuleFor(request => request.Content).NotEmpty().Length(5, 150);
        RuleFor(request => request.Type).NotEmpty().IsInEnum();
    }
}
