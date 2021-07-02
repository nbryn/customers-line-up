using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Bookings
{
    public class BusinessBookings
    {
        public class Query : IRequest<Result<IList<BookingDTO>>>
        {
            public string BusinessId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(b => b.BusinessId).NotNull();
            }
        }

        public class Handler : IRequestHandler<Query, Result<IList<BookingDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<IList<BookingDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId);

                if (business == null)
                {
                    return Result.NotFound<IList<BookingDTO>>();
                }

                var bookings = _context.Bookings
                                        .Include(x => x.TimeSlot)
                                        .Include(x => x.TimeSlot.Business)
                                        .Where(x => x.BusinessId == query.BusinessId);

                var result = await _mapper.ProjectTo<BookingDTO>(bookings).ToListAsync();

                return Result.Ok<IList<BookingDTO>>(result);
            }
        }
    }
}