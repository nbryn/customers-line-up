using System.Collections.Generic;
using CLup.Application.Queries.Shared;

namespace CLup.Application.Queries.User.Message
{
    public class FetchMessagesResponse
    {
        public string UserId { get; set; }
        
        public IList<SentMessageDto> SentMessages { get; set; }

        public IList<ReceivedMessageDto> ReceivedMessages { get; set; }
    }
}