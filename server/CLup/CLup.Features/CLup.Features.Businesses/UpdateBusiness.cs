using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;

namespace CLup.Features.Businesses
{
    public class UpdateBusiness
    {
        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Zip { get; set; }
            public string Address { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string OwnerEmail { get; set; }
            public string Opens { get; set; }
            public string Closes { get; set; }
            public int Capacity { get; set; }
            public int TimeSlotLength { get; set; }
            public string Type { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.OwnerEmail).EmailAddress();
                RuleFor(x => x.Zip).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.Opens).NotEmpty();
                RuleFor(x => x.Closes).NotEmpty();
                RuleFor(x => x.Capacity).NotEmpty();
                RuleFor(x => x.TimeSlotLength).NotEmpty();
                RuleFor(x => x.Type).IsEnumName(typeof(BusinessType));
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
                var business = await _context.Businesses.FirstOrDefaultAsync(x => x.Id == command.Id);

                if (business == null)
                {
                    return Result.NotFound();
                }

                var updatedBusiness = _mapper.Map<Business>(command);
                updatedBusiness.Id = business.Id;

                _context.Entry(business).CurrentValues.SetValues(updatedBusiness);

                await _context.SaveChangesAsync();

                return Result.Ok();
            }
        }
    }

}

