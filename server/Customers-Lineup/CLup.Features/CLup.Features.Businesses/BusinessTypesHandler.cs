using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using CLup.Domain;
using CLup.Features.Businesses.Queries;
using CLup.Features.Util;

namespace CLup.Features.Businesses
{

    public class BusinessTypesHandler : IRequestHandler<BusinessTypesQuery, IList<string>>
    {

        public Task<IList<string>> Handle(BusinessTypesQuery query, CancellationToken cancellationToken)
        {
            var values = new List<string>();
            var types = EnumUtil.GetValues<BusinessType>();

            foreach (BusinessType type in types)
            {
                values.Add(type.ToString("G"));
            }

            return Task.FromResult<IList<string>>(values);
        }
    }
}

