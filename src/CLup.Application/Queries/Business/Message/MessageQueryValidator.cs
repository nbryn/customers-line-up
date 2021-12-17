using FluentValidation;

namespace CLup.Application.Queries.Business.Message
{
    public class MessageQueryValidator : AbstractValidator<FetchMessagesQuery>
    {
        public MessageQueryValidator()
        {
            RuleFor(q => q.BusinessId).NotEmpty();
        }
    }
}