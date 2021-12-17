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

namespace CLup.Application.Queries.Business.Message
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
            return await _queryContext.Businesses.FirstOrDefaultAsync(b => b.OwnerEmail == query.UserEmail)
                .ToResult()
                .EnsureDiscard(u => u.Id == query.BusinessId, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages = await _queryContext.UserMessages.Include(um => um.Receiver).Where(um => um.SenderId == query.BusinessId).ToListAsync();
                    var receivedMessages = await _queryContext.BusinessMessages.Include(um => um.Sender).Where(bm => bm.ReceiverId == query.BusinessId).ToListAsync();

                    return new FetchMessagesResponse()
                    {
                        BusinessId = query.BusinessId,
                        SentMessages = sentMessages.Select(_mapper.Map<SentMessageDto>).ToList(),
                        ReceivedMessages = receivedMessages.Select(_mapper.Map<ReceivedMessageDto>).ToList()
                    };
                })
                .Finally(response => response);
        }
    }
}