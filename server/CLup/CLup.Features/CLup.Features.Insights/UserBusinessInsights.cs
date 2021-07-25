using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Insights
{
    public class UserBusinessInsights
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
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Model>> Handle(Query query, CancellationToken cancellationToken)
            {

                var businesses = await _context.Businesses
                                         .Where(b => b.OwnerEmail == query.UserEmail)
                                         .ToListAsync();

                var businessIds = businesses.Select(b => b.Id);

                var bookings = await _context.Bookings
                                       .Where(x => businessIds.Contains(x.BusinessId))
                                       .ToListAsync();

                var employees = await _context.Employees
                                        .Where(e => businessIds.Contains(e.BusinessId))
                                        .ToListAsync();


                var insights = new Model
                {
                    BusinessBookings = bookings.Count,
                    Businesses = businesses.Count,
                    Employees = employees.Count
                };

                return Result.Ok<Model>(insights);
            }
        }
    }
}

