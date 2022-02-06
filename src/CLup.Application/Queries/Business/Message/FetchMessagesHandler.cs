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
    public class FetchMessagesHandler : IRequestHandler<FetchBusinessMessagesQuery, Result<FetchBusinessMessagesResponse>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public FetchMessagesHandler(
            IReadOnlyDbContext readOnlyContext,
            IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }

        public async Task<Result<FetchBusinessMessagesResponse>> Handle(
            FetchBusinessMessagesQuery query,
            CancellationToken cancellationToken)
        {
            return await _readOnlyContext.Businesses
                .FirstOrDefaultAsync(business => business.OwnerEmail == query.UserEmail && business.Id == query.BusinessId)
                .ToResult()
                .EnsureDiscard(business => business != null, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages = await _readOnlyContext.Messages
                    .Include(um => um.Metadata)
                    .Where(um => um.SenderId == query.BusinessId && !um.Metadata.DeletedBySender)
                    .ToListAsync();

                    var sentMessagesDto = sentMessages.Select(async message =>
                    {
                        var dto = _mapper.Map<MessageDto>(message);
                        var sender = await _readOnlyContext.Businesses.FirstOrDefaultAsync(business => business.Id == query.BusinessId);
                        var receiver = await _readOnlyContext.Users.FirstOrDefaultAsync(user => user.Id == message.ReceiverId);
                        dto.Sender = sender.Name;
                        dto.Receiver = receiver.Name;

                        return dto;
                    })
                    .ToList();

                    var receivedMessages = await _readOnlyContext.Messages
                    .Include(um => um.Metadata)
                    .Where(bm => bm.ReceiverId == query.BusinessId && !bm.Metadata.DeletedByReceiver)
                    .ToListAsync();

                    var receivedMessagesDto = receivedMessages.Select(async message =>
                    {
                        var dto = _mapper.Map<MessageDto>(message);
                        var sender = await _readOnlyContext.Users.FirstOrDefaultAsync(user => user.Id == message.SenderId);
                        var receiver = await _readOnlyContext.Businesses.FirstOrDefaultAsync(business => business.Id == query.BusinessId);
                        dto.Sender = sender.Name;
                        dto.Receiver = receiver.Name;

                        return dto;
                    })
                    .ToList();

                    return new FetchBusinessMessagesResponse()
                    {
                        BusinessId = query.BusinessId,
                        SentMessages = await Task.WhenAll(sentMessagesDto),
                        ReceivedMessages = await Task.WhenAll(receivedMessagesDto)
                    };
                })
                .Finally(response => response);
        }
    }
}