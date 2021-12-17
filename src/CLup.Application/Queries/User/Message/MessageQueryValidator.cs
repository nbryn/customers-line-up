using FluentValidation;

namespace CLup.Application.Queries.User.Message
{
    public class MessageQueryValidator : AbstractValidator<FetchMessagesQuery>
    {
        public MessageQueryValidator()
        {
            RuleFor(q => q.UserId).NotEmpty();
        }
    }
}