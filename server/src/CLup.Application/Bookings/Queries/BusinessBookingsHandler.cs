using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Extensions;
using CLup.Application.Shared;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Bookings.Queries
{

    public class BusinessBookingsHandler : IRequestHandler<BusinessBookingsQuery, Result<List<BookingDto>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public BusinessBookingsHandler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<BookingDto>>> Handle(BusinessBookingsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .ToResult()
                    .EnsureDiscard(business => business != null)
                    .Finally(async () =>
                    {
                        var bookings = await _context.Bookings
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
