using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;


namespace CLup.Features.TimeSlots
{
    public class GenerateTimeSlots
    {
        public class Command : IRequest<Result>
        {

            public string BusinessId { get; set; }

            public DateTime Start { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.BusinessId).NotEmpty();
                RuleFor(x => x.Start).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;
            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == command.BusinessId);

                if (business == null)
                {
                    return Result.NotFound();
                }

                var existingTimeSlots = await _context.TimeSlots.Include(x => x.Bookings)
                                                                .Include(x => x.Business)
                                                                .Where(x => x.BusinessId == command.BusinessId && x.Start.Date == command.Start.Date)
                                                                .ToListAsync();

                if (existingTimeSlots.Any())
                {
                    return Result.Conflict("Time slots already generated for this date");
                }

                var opens = command.Start.AddHours(Double.Parse(business.Opens.Substring(0, business.Opens.IndexOf("."))));
                var closes = command.Start.AddHours(Double.Parse(business.Closes.Substring(0, business.Closes.IndexOf("."))));

                for (var date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(business.TimeSlotLength))
                {
                    // Only add TimeSlots when shop is open
                    /*         if (date.Equals(closingTime))
                            {
                                closingTime = closingTime.AddHours(24);

                                date = date.AddHours((23 - date.Hour) + Double.Parse(business.Opens));

                                continue;
                            } */

                    var timeSlot = _mapper.Map<TimeSlot>(business);

                    timeSlot.Start = date;
                    timeSlot.End = date.AddMinutes(business.TimeSlotLength);

                    await _context.TimeSlots.AddAsync(timeSlot);
                    await _context.SaveChangesAsync();
                }

                return Result.Ok();
            }
        }
    }
}

