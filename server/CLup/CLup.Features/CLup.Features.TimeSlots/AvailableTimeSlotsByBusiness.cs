using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.TimeSlots
{
    public class AvailableTimeSlotsByBusiness
    {
        public class Query : IRequest<Result<List<TimeSlotDTO>>>
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

        public class Handler : IRequestHandler<Query, Result<List<TimeSlotDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;
            public Handler(CLupContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<TimeSlotDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                        .FailureIfDiscard("Business not found.")
                        .AndThen(() => _context.TimeSlots
                                            .Include(x => x.Bookings)
                                            .Include(x => x.Business)
                                            .Where(x => x.BusinessId == query.BusinessId))
                                            
                        .AndThen(timeSlots => _mapper.ProjectTo<TimeSlotDTO>(timeSlots).ToListAsync());
            }
        }
    }
}

