using System.Collections.Generic;

using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Businesses.Queries
{
    public class BusinessesByOwnerQuery : IRequest<Result<IList<BusinessDTO>>>
    {
        public string OwnerEmail { get; set; }
        public BusinessesByOwnerQuery(string ownerEmail) => OwnerEmail = ownerEmail;
    }
}



