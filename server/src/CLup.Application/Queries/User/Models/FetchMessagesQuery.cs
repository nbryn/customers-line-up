using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.User.Models
{
    public class FetchMessagesQuery : IRequest<Result<FetchMessagesResponse>>
    {
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public FetchMessagesQuery(string userId) => UserId = userId;
    }
}

