using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Users.Queries
{
    public class FetchMessagesQuery : IRequest<Result<UserDTO>>
    {
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public FetchMessagesQuery(string userId) => UserId = userId;
    }
}

