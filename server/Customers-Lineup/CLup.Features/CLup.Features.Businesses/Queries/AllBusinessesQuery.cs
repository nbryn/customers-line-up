using System.Collections.Generic;

using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Businesses.Queries
{
    public class AllBusinessesQuery : IRequest<Result<IList<BusinessDTO>>>
    {

    }
}