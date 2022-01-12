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
    public class FetchMessagesHandler : IRequestHandler<FetchBusinessMessagesQuery, Result<FetchMessagesResponse>>
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
            FetchBusinessMessagesQuery query,
            CancellationToken cancellationToken)
        {
            return await _queryContext.Businesses
                .FirstOrDefaultAsync(business => business.OwnerEmail == query.UserEmail && business.Id == query.BusinessId)
                .ToResult()
                .EnsureDiscard(business => business != null, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages = await _queryContext.BusinessMessages
                    .Include(um => um.Metadata)
                    .Include(um => um.Sender)
                    .Include(um => um.Receiver)
                    .Where(um => um.Sender.Id == query.BusinessId)
                    .ToListAsync();

                    var receivedMessages = await _queryContext.UserMessages
                    .Include(bm => bm.Metadata)
                    .Include(bm => bm.Sender)
                    .Include(bm => bm.Receiver)
                    .Where(bm => bm.ReceiverId == query.BusinessId)
                    .ToListAsync();

                    return new FetchMessagesResponse()
                    {
                        BusinessId = query.BusinessId,
                        SentMessages = sentMessages.Select(_mapper.Map<MessageDto>).ToList(),
                        ReceivedMessages = receivedMessages.Select(_mapper.Map<MessageDto>).ToList()
                    };
                })
                .Finally(response => response);
        }
    }
}