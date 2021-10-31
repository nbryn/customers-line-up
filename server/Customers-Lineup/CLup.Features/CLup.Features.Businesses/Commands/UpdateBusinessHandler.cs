using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain.Businesses;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.Businesses.Commands
{
    public class UpdateBusinessHandler : IRequestHandler<UpdateBusinessCommand, Result>
    {
        private readonly IValidator<Business> _businessValidator;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UpdateBusinessHandler(
            IValidator<Business> businessValidator,
            CLupContext context, 
            IMapper mapper)
        {
            _businessValidator = businessValidator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
        {
            return await _context.Businesses.FirstOrDefaultAsync(x => x.Id == command.Id)
                    .FailureIfDiscard("Business not found.")
                    .AndThen(() => _mapper.Map<Business>(command))
                    .Validate(_businessValidator)
                    .Finally(updatedBusiness => _context.UpdateEntity(command.Id, updatedBusiness));
        }
    }
}