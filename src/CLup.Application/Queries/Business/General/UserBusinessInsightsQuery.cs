using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.General
{
    public class UserBusinessInsightsQuery
    {
        public class Query : IRequest<Result<Model>>
        {
            public string UserEmail { get; set; }

            public Query(string userEmail) => UserEmail = userEmail;
        }

        public class Model
        {
            public int BusinessBookings { get; set; }
            public int Businesses { get; set; }
            public int Employees { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Model>>
        {
            private readonly IReadOnlyDbContext _readOnlyReadOnlyContext;

            public Handler(IReadOnlyDbContext readOnlyContext) => _readOnlyReadOnlyContext = readOnlyContext;
      
            public async Task<Result<Model>> Handle(Query query, CancellationToken cancellationToken)
            {

                var businesses = await _readOnlyReadOnlyContext.Businesses
                                         .Where(b => b.OwnerEmail == query.UserEmail)
                                         .ToListAsync();

                var businessIds = businesses.Select(b => b.Id);

                var bookings = await _readOnlyReadOnlyContext.Bookings
                                       .Where(x => businessIds.Contains(x.BusinessId))
                                       .ToListAsync();

                var employees = await _readOnlyReadOnlyContext.Employees
                                        .Where(e => businessIds.Contains(e.BusinessId))
                                        .ToListAsync();


                var insights = new Model
                {
                    BusinessBookings = bookings?.Count ?? 0,
                    Businesses = businesses?.Count ?? 0,
                    Employees = employees?.Count ?? 0
                };

                return Result.Ok(insights);
            }
        }
    }
}

