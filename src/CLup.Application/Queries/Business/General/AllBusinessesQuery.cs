using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.Business.General
{
    public class AllBusinessesQuery : IRequest<Result<IList<BusinessDto>>>
    {

    }
}