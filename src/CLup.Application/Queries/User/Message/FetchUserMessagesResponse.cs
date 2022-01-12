using System.Collections.Generic;
using CLup.Application.Queries.Shared;

namespace CLup.Application.Queries.User.Message
{
    public class FetchUserMessagesResponse
    {
        public string UserId { get; set; }
        
        public IList<MessageDto> SentMessages { get; set; }

        public IList<MessageDto> ReceivedMessages { get; set; }
    }
}