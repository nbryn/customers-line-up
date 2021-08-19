using System.Collections.Generic;

using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Businesses.Queries
{
    public class AllBusinessesQuery : IRequest<Result<IList<BusinessDTO>>>
    {

    }
}