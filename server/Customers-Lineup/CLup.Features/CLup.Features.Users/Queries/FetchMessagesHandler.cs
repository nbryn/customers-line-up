using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Shared;
using CLup.Features.Extensions;
using CLup.Features.Users.Responses;

namespace CLup.Features.Users.Queries
{

    public class FetchMessagesHandler : IRequestHandler<FetchMessagesQuery, Result<FetchMessagesResponse>>
    {
        private readonly IUserService _userService;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public FetchMessagesHandler(IUserService userService, CLupContext context, IMapper mapper)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<FetchMessagesResponse>> Handle(FetchMessagesQuery query, CancellationToken cancellationToken)
        {
            // Check if email matches requested user id
            var sentMessages = await _context.UserMessages.Where(um => um.SenderId == query.UserId).ToListAsync();
            var receivedMessages = await _context.BusinessMessages.Where(bm => bm.ReceiverId == query.UserId).ToListAsync();

            var response = new FetchMessagesResponse()
            {
                UserId = query.UserId,
                SentMessages = sentMessages,
                ReceivedMessages = receivedMessages
            };

            return response;
        }
    }
}