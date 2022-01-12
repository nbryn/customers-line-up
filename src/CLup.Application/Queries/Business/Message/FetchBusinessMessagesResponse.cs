using System.Collections.Generic;
using CLup.Application.Queries.Shared;

namespace CLup.Application.Queries.Business.Message
{
    public class FetchMessagesResponse
    {
        public string BusinessId { get; set; }
        
        public IList<MessageDto> SentMessages { get; set; }

        public IList<MessageDto> ReceivedMessages { get; set; }
    }
}