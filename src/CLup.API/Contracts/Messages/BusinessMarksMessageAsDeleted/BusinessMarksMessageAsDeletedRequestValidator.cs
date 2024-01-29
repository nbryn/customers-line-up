using FluentValidation;

namespace CLup.API.Contracts.Messages.BusinessMarksMessageAsDeleted;

public class BusinessMarksMessageAsDeletedRequestValidator : AbstractValidator<BusinessMarksMessageAsDeletedRequest>
{
    public BusinessMarksMessageAsDeletedRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotNull();
        RuleFor(request => request.MessageId).NotNull();
        RuleFor(request => request.ForSender).NotNull();
    }
}
