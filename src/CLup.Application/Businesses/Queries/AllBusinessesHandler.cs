using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.Queries
{

    public class AllBusinessesHandler : IRequestHandler<AllBusinessesQuery, Result<IList<BusinessDto>>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public AllBusinessesHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDto>>> Handle(AllBusinessesQuery query, CancellationToken cancellationToken)
        {
            var businesses = await _readOnlyContext.Businesses
                .Include(business => business.Bookings)
                    .ThenInclude(booking => booking.TimeSlot)
                    .ThenInclude(timeSlot => timeSlot.Business)
                .Include(business => business.Bookings)
                    .ThenInclude(booking => booking.User)
                .AsSplitQuery()
                .ToListAsync(); 

            return Result.Ok<IList<BusinessDto>>(_mapper.Map<IList<BusinessDto>>(businesses));
        }
    }
}
