using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Business;
using CLup.Domain.Businesses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.Business.Create
{
    public class CreateBusinessHandler : IRequestHandler<CreateBusinessCommand, Result>
    {
        private readonly IValidator<Domain.Businesses.Business> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public CreateBusinessHandler(
            IValidator<Domain.Businesses.Business> validator,
            ICLupDbContext context, 
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
                    .AndThen(() => _mapper.Map<Domain.Businesses.Business>(command))
                    .Validate(_validator)
                    .Finally(business => _context.AddAndSave(business));
        }
    }
}