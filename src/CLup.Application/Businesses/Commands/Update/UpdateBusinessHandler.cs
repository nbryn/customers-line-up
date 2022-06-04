using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using FluentValidation;
using MediatR;

namespace CLup.Application.Businesses.Commands.Update
{
    public class UpdateBusinessHandler : IRequestHandler<UpdateBusinessCommand, Result>
    {
        private readonly IValidator<Business> _businessValidator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBusinessHandler(
            IValidator<Business> businessValidator,
            ICLupDbContext context,
            IMapper mapper)
        {
            _businessValidator = businessValidator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .EnsureDiscard(user => user.GetBusiness(command.Id) != null, "Business not found.")
                .AndThen(() => _mapper.Map<Business>(command))
                .Validate(_businessValidator)
                .Finally(updatedBusiness => _context.UpdateEntity(command.Id, updatedBusiness));
    }
}