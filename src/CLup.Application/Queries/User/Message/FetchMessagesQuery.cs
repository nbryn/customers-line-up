using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.User.Message
{
    public class FetchMessagesQuery : IRequest<Result<FetchMessagesResponse>>
    {

        public FetchMessagesQuery()
        {

        }
        
        public string UserId { get; set; }

        public FetchMessagesQuery(string userId) => UserId = userId;
    }
}

