using FluentValidation;

namespace CLup.API.Contracts.Businesses.GetBusiness;

public sealed class GetBusinessRequestValidator : AbstractValidator<GetBusinessRequest>
{
    public GetBusinessRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
    }
}
