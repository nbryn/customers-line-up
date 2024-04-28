namespace CLup.API.Businesses.Contracts.GetBusinessAggregate;

public sealed class GetBusinessAggregateRequestValidator : AbstractValidator<GetBusinessAggregateRequest>
{
    public GetBusinessAggregateRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
    }
}
