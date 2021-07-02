using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using CLup.Domain;
using CLup.Features.Util;

namespace CLup.Features.Businesses
{
    public class BusinessTypes
    {
        public class Query : IRequest<IList<string>>
        {

        }
        public class Handler : IRequestHandler<Query, IList<string>>
        {

            public Task<IList<string>> Handle(Query query, CancellationToken cancellationToken)
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
}

