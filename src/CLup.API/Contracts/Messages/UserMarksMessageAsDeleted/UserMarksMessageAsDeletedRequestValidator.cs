using CLup.Application.Messages.Commands.UserMarksMessageAsDeleted;
using FluentValidation;

namespace CLup.API.Contracts.Messages.UserMarksMessageAsDeleted;

public sealed class UserMarksMessageAsDeletedRequestValidator : AbstractValidator<UserMarksMessageAsDeletedCommand>
{
    public UserMarksMessageAsDeletedRequestValidator()
    {
        RuleFor(request => request.MessageId).NotNull();
        RuleFor(request => request.ForSender).NotNull();
    }
}
