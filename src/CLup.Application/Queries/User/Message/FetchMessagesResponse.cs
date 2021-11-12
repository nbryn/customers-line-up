using System.Collections.Generic;
using CLup.Domain.Business;
using CLup.Domain.User;

namespace CLup.Application.Queries.User.Message
{
    public class FetchMessagesResponse
    {
        public string UserId { get; set; }
        
        public IList<UserMessage> SentMessages { get; set; }

        public IList<BusinessMessage> ReceivedMessages { get; set; }
    }
}