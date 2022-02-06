using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.Business.Message
{
    public class FetchBusinessMessagesQuery : IRequest<Result<FetchBusinessMessagesResponse>>
    {

        public FetchBusinessMessagesQuery()
        {

        }
        
        public string BusinessId { get; set; }
        public string UserEmail { get; set; }

        public FetchBusinessMessagesQuery(string businessId) => BusinessId = businessId;
    }
}

