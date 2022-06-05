using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Messages;
using FluentValidation;
using MediatR;

namespace CLup.Application.Messages.Send
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, Result>
    {
        private readonly IValidator<Message> _validator;
        private readonly ICLupRepository _repository;
        private readonly IMapper _mapper;

        public SendMessageHandler(
            IValidator<Message> validator,
            ICLupRepository repository,
            IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(SendMessageCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.SenderId)
                .ToResult()
                .AndThen(async user =>
                {
                    var business = await _repository.FetchBusiness(command.SenderId);
                    return new { business, user };
                })
                .Ensure(entry => entry.user != null || entry.business != null, "Invalid sender.")
                .AndThen(async _ =>
                {
                    var user = await _repository.FetchUserAggregate(command.ReceiverId);
                    var business = await _repository.FetchBusiness(command.ReceiverId);
                    return new { business, user };
                })
                .Ensure(entry => entry.user != null || entry.business != null, "Invalid receiver.")
                .AndThen(_ => _mapper.Map<Message>(command))
                .Validate(_validator)
                .Finally(message => _repository.AddAndSave(message));
    }
}