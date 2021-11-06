using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.Queries
{
    public class BusinessesByOwnerQuery : IRequest<Result<IList<BusinessDTO>>>
    {
        public string OwnerEmail { get; set; }
        public BusinessesByOwnerQuery(string ownerEmail) => OwnerEmail = ownerEmail;
    }
}



