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
        private readonly ICLupRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBusinessHandler(
            IValidator<Business> businessValidator,
            ICLupRepository repository,
            IMapper mapper)
        {
            _businessValidator = businessValidator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .Ensure(user => user.GetBusiness(command.Id) != null, "Business not found.")
                .AndThen(_ => _mapper.Map<Business>(command))
                .Validate(_businessValidator)
                .Finally(updatedBusiness => _repository.UpdateEntity(command.Id, updatedBusiness));
    }
}