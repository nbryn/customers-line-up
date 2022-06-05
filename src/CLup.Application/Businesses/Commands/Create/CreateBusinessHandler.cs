using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;

namespace CLup.Application.Businesses.Commands.Create
{
    public class CreateBusinessHandler : IRequestHandler<CreateBusinessCommand, Result>
    {
        private readonly IValidator<Business> _validator;
        private readonly ICLupRepository _repository;
        private readonly IMapper _mapper;

        public CreateBusinessHandler(
            IValidator<Business> validator,
            ICLupRepository repository,
            IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .AndThen(user => user.UpdateRole(Role.Owner))
                .AndThen(_ => _mapper.Map<Business>(command))
                .Validate(_validator)
                .Finally(business => _repository.AddAndSave(business));
    }
}