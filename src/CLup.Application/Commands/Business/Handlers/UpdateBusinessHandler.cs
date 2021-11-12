using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Commands.Business.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.Business.Handlers
{
    public class UpdateBusinessHandler : IRequestHandler<UpdateBusinessCommand, Result>
    {
        private readonly IValidator<Domain.Business.Business> _businessValidator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBusinessHandler(
            IValidator<Domain.Business.Business> businessValidator,
            ICLupDbContext context, 
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
                    .AndThen(() => _mapper.Map<Domain.Business.Business>(command))
                    .Validate(_businessValidator)
                    .Finally(updatedBusiness => _context.UpdateEntity(command.Id, updatedBusiness));
        }
    }
}