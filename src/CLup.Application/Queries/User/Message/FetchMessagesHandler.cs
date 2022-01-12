using System;
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
    public class FetchMessagesHandler : IRequestHandler<FetchUserMessagesQuery, Result<FetchUserMessagesResponse>>
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

        public async Task<Result<FetchUserMessagesResponse>> Handle(
            FetchUserMessagesQuery query,
            CancellationToken cancellationToken)
        {
            return await _queryContext.Users.FirstOrDefaultAsync(user => user.Id == query.UserId)
                .ToResult()
                .EnsureDiscard(user => user?.Id == query.UserId, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages = await _queryContext.UserMessages
                    .Include(um => um.Metadata)
                    .Include(um => um.Sender)
                    .Include(um => um.Receiver)
                    .Where(um => um.Sender.Id == query.UserId && !um.Metadata.DeletedBySender)
                    .ToListAsync();

                    var receivedMessages = await _queryContext.BusinessMessages
                    .Include(um => um.Metadata)
                    .Include(um => um.Sender)
                    .Include(um => um.Receiver)
                    .Where(bm => bm.Receiver.Id == query.UserId && !bm.Metadata.DeletedByReceiver)
                    .ToListAsync();

                    Console.WriteLine(receivedMessages[0].Sender?.Id);

                    return new FetchUserMessagesResponse()
                    {
                        UserId = query.UserId,
                        SentMessages = sentMessages.Select(_mapper.Map<MessageDto>).ToList(),
                        ReceivedMessages = receivedMessages.Select(_mapper.Map<MessageDto>).ToList()
                    };
                })
                .Finally(response => response);
        }
    }
}