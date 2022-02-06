using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Queries.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.Booking
{

    public class BusinessBookingsHandler : IRequestHandler<BusinessBookingsQuery, Result<List<BookingDto>>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public BusinessBookingsHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _mapper = mapper;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<Result<List<BookingDto>>> Handle(BusinessBookingsQuery query, CancellationToken cancellationToken)
        {
            return await _readOnlyContext.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .ToResult()
                    .EnsureDiscard(business => business != null)
                    .Finally(async () =>
                    {
                        var bookings = await _readOnlyContext.Bookings
                                        .Include(x => x.TimeSlot)
                                        .ThenInclude(x => x.Business)
                                        .Include(x => x.User)
                                        .Where(x => x.BusinessId == query.BusinessId)
                                        .OrderBy(x => x.TimeSlot.Start).ToListAsync();

                        return bookings.Select(_mapper.Map<BookingDto>).ToList();
                    });
        }
    }
}
