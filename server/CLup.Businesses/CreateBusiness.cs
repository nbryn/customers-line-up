using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Context;
using CLup.Util;

namespace CLup.Businesses
{
    public class CreateBusiness
    {
        public class Command : IRequest<Result>
        {
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
                var owner = await _context.BusinessOwners.FirstOrDefaultAsync(o => o.UserEmail == command.OwnerEmail);

                if (owner == null)
                {
                    var newOwner = new BusinessOwner
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserEmail = command.OwnerEmail,
                    };

                    _context.BusinessOwners.Add(newOwner);
                }

                BusinessType.TryParse(command.Type, out BusinessType type);

                var newBusiness = _mapper.Map<Business>(command);
                newBusiness.Id = Guid.NewGuid().ToString();
                _context.Businesses.Add(newBusiness);

                await _context.SaveChangesAsync();

                return Result.Created();
            }
        }
    }

}
}
