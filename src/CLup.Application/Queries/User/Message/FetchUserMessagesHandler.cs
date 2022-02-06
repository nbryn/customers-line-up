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
    public class FetchUserMessagesHandler : IRequestHandler<FetchUserMessagesQuery, Result<FetchUserMessagesResponse>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public FetchUserMessagesHandler(
            IReadOnlyDbContext readOnlyContext,
            IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }

        public async Task<Result<FetchUserMessagesResponse>> Handle(
            FetchUserMessagesQuery query,
            CancellationToken cancellationToken)
        {
            return await _readOnlyContext.Users.FirstOrDefaultAsync(user => user.Id == query.UserId)
                .ToResult()
                .EnsureDiscard(user => user?.Email == query.RequesterEmail, "You don't have access to these messages")
                .AndThen(async () =>
                {
                    var sentMessages = await _readOnlyContext.Messages
                    .Include(um => um.Metadata)
                    .Where(um => um.SenderId == query.UserId && !um.Metadata.DeletedBySender)
                    .ToListAsync();

                    var sentMessagesDto = sentMessages.Select(async message =>
                    {
                        var dto = _mapper.Map<MessageDto>(message);
                        var sender = await _readOnlyContext.Users.FirstOrDefaultAsync(user => user.Id == query.UserId);
                        var receiver = await _readOnlyContext.Businesses.FirstOrDefaultAsync(business => business.Id == message.ReceiverId);
                        dto.Sender = sender.Name;
                        dto.Receiver = receiver.Name;

                        return dto;
                    })
                    .ToList();

                    var receivedMessages = await _readOnlyContext.Messages
                    .Include(um => um.Metadata)
                    .Where(bm => bm.ReceiverId == query.UserId && !bm.Metadata.DeletedByReceiver)
                    .ToListAsync();

                    var receivedMessagesDto = receivedMessages.Select(async message =>
                    {
                        var dto = _mapper.Map<MessageDto>(message);
                        var sender = await _readOnlyContext.Businesses.FirstOrDefaultAsync(business => business.Id == message.SenderId);
                        var receiver = await _readOnlyContext.Users.FirstOrDefaultAsync(user => user.Id == query.UserId);
                        dto.Sender = sender.Name;
                        dto.Receiver = receiver.Name;

                        return dto;
                    })
                    .ToList();

                    return new FetchUserMessagesResponse()
                    {
                        UserId = query.UserId,
                        SentMessages = await Task.WhenAll(sentMessagesDto),
                        ReceivedMessages = await Task.WhenAll(receivedMessagesDto)
                    };
                })
                .Finally(response => response);
        }
    }
}