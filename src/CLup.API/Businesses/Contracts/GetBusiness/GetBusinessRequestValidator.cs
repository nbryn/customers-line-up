namespace CLup.API.Businesses.Contracts.GetBusiness;

public sealed class GetBusinessRequestValidator : AbstractValidator<GetBusinessRequest>
{
    public GetBusinessRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
    }
}
