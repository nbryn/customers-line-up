using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Context;
using CLup.Util;

namespace CLup.TimeSlots
{
    public class AvailableTimeSlotsByBusiness
    {
        public class Query : IRequest<Result<IList<TimeSlotDTO>>>
        {

            public string BusinessId { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.BusinessId).NotEmpty();
                RuleFor(x => x.Start).NotEmpty();
                RuleFor(x => x.End).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Result<IList<TimeSlotDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;
            public Handler(CLupContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<IList<TimeSlotDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var business = _context.Businesses.FirstOrDefault(b => b.Id == query.BusinessId);

                if (business == null)
                {
                    return Result.NotFound<IList<TimeSlotDTO>>();
                }

                var timeSlots = _context.TimeSlots
                                            .Include(x => x.Bookings)
                                            .Include(x => x.Business)
                                            .Where(x => x.BusinessId == query.BusinessId);

                var result = await _mapper.ProjectTo<TimeSlotDTO>(timeSlots).ToListAsync();

                return Result.Ok<IList<TimeSlotDTO>>(result);
            }
        }
    }
}

