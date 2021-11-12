using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.Business.Owner
{
    public class BusinessesByOwnerQuery : IRequest<Result<IList<BusinessDto>>>
    {
        public string OwnerEmail { get; set; }
        public BusinessesByOwnerQuery(string ownerEmail) => OwnerEmail = ownerEmail;
    }
}



