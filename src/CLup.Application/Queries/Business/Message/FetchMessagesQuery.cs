using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.Business.Message
{
    public class FetchMessagesQuery : IRequest<Result<FetchMessagesResponse>>
    {

        public FetchMessagesQuery()
        {

        }
        
        public string BusinessId { get; set; }
        public string UserEmail { get; set; }

        public FetchMessagesQuery(string businessId) => BusinessId = businessId;
    }
}

