using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.Bookings.Queries
{

    public class BusinessBookingsHandler : IRequestHandler<BusinessBookingsQuery, Result<List<BookingDTO>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public BusinessBookingsHandler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<BookingDTO>>> Handle(BusinessBookingsQuery query, CancellationToken cancellationToken)
        {

            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .ToResult()
                    .EnsureDiscard(business => business != null)
                    .Finally(async () =>
                    {
                        var bookings = await _context.Bookings
                                        .Include(x => x.TimeSlot)
                                        .Include(x => x.TimeSlot.Business)
                                        .Where(x => x.BusinessId == query.BusinessId)
                                        .OrderBy(x => x.TimeSlot.Start).ToListAsync();

                        return bookings.Select(_mapper.Map<BookingDTO>).ToList();
                    });
        }
    }
}
