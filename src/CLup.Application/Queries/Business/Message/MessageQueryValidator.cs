using FluentValidation;

namespace CLup.Application.Queries.Business.Message
{
    public class MessageQueryValidator : AbstractValidator<FetchBusinessMessagesQuery>
    {
        public MessageQueryValidator()
        {
            RuleFor(q => q.BusinessId).NotEmpty();
        }
    }
}