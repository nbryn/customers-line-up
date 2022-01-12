using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.Business.Message
{
    public class FetchBusinessMessagesQuery : IRequest<Result<FetchMessagesResponse>>
    {

        public FetchBusinessMessagesQuery()
        {

        }
        
        public string BusinessId { get; set; }
        public string UserEmail { get; set; }

        public FetchBusinessMessagesQuery(string businessId) => BusinessId = businessId;
    }
}

