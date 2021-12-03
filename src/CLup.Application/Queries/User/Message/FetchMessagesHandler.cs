using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Queries.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User.Message
{
    public class FetchMessagesHandler : IRequestHandler<FetchMessagesQuery, Result<FetchMessagesResponse>>
    {
        private readonly IQueryDbContext _queryContext;
        private readonly IMapper _mapper;

        public FetchMessagesHandler(
            IQueryDbContext queryContext,
            IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<Result<FetchMessagesResponse>> Handle(
            FetchMessagesQuery query,
            CancellationToken cancellationToken)
        {
            return await _queryContext.Users.FirstOrDefaultAsync(u => u.Id == query.UserId)
                .ToResult()
                .EnsureDiscard(u => u.Id == query.UserId, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages = await _queryContext.UserMessages.Include(um => um.Receiver).Where(um => um.SenderId == query.UserId).ToListAsync();
                    var receivedMessages = await _queryContext.BusinessMessages.Include(um => um.Sender).Where(bm => bm.ReceiverId == query.UserId).ToListAsync();

                    return new FetchMessagesResponse()
                    {
                        UserId = query.UserId,
                        SentMessages = sentMessages.Select(_mapper.Map<SentMessageDto>).ToList(),
                        ReceivedMessages = receivedMessages.Select(_mapper.Map<ReceivedMessageDto>).ToList()
                    };
                })
                .Finally(response => response);
        }
    }
}