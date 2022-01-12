using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.User.Message
{
    public class FetchUserMessagesQuery : IRequest<Result<FetchUserMessagesResponse>>
    {
        public string UserId { get; set; }

        public FetchUserMessagesQuery()
        {

        }
        
        public FetchUserMessagesQuery(string userId) => UserId = userId;
    }
}

