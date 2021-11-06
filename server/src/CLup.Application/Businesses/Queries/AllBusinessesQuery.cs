using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.Queries
{
    public class AllBusinessesQuery : IRequest<Result<IList<BusinessDTO>>>
    {

    }
}