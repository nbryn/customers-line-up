namespace CLup.API.Contracts.Businesses.GetBusinessAggregate;

public sealed class GetBusinessAggregateRequestValidator : AbstractValidator<GetBusinessAggregateRequest>
{
    public GetBusinessAggregateRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
    }
}
