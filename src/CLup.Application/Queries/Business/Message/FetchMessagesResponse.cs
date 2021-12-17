using System.Collections.Generic;
using CLup.Application.Queries.Shared;

namespace CLup.Application.Queries.Business.Message
{
    public class FetchMessagesResponse
    {
        public string BusinessId { get; set; }
        
        public IList<SentMessageDto> SentMessages { get; set; }

        public IList<ReceivedMessageDto> ReceivedMessages { get; set; }
    }
}