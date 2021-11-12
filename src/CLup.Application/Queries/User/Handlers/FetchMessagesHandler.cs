using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Queries.User.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User.Handlers
{
    public class FetchMessagesHandler : IRequestHandler<FetchMessagesQuery, Result<FetchMessagesResponse>>
    {
        private readonly IQueryDbContext _queryContext;

        public FetchMessagesHandler(IQueryDbContext queryContext) => _queryContext = queryContext;
        
        public async Task<Result<FetchMessagesResponse>> Handle(
            FetchMessagesQuery query,
            CancellationToken cancellationToken)
        {
            return await _queryContext.Users.FirstOrDefaultAsync(u => u.Email == query.UserEmail)
                .ToResult()
                .EnsureDiscard(u => u.Id == query.UserId, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages =
                        await _queryContext.UserMessages.Where(um => um.SenderId == query.UserId).ToListAsync();
                    var receivedMessages = await _queryContext.BusinessMessages.Where(bm => bm.ReceiverId == query.UserId)
                        .ToListAsync();

                    return new FetchMessagesResponse()
                    {
                        UserId = query.UserId,
                        SentMessages = sentMessages,
                        ReceivedMessages = receivedMessages
                    };
                })
                .Finally(response => response);
        }
    }
}