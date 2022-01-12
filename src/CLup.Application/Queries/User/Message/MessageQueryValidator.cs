using FluentValidation;

namespace CLup.Application.Queries.User.Message
{
    public class MessageQueryValidator : AbstractValidator<FetchUserMessagesQuery>
    {
        public MessageQueryValidator()
        {
            RuleFor(q => q.UserId).NotEmpty();
        }
    }
}