using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Businesses.Commands
{
    public class CreateBusinessHandler : IRequestHandler<CreateBusinessCommand, Result>
    {
        private readonly IValidator<Business> _validator;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public CreateBusinessHandler(
            IValidator<Business> validator,
            CLupContext context, 
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
        {
            return await _context.BusinessOwners.FirstOrDefaultAsync(o => o.UserEmail == command.OwnerEmail)
                    .ToResult()
                    .AndThen(owner => _context.CreateEntityIfNotExists(owner, new BusinessOwner(command.OwnerEmail)))
                    .AndThen(() => _mapper.Map<Business>(command))
                    .Validate(_validator)
                    .Finally(business => _context.AddAndSave(business));
        }
    }
}