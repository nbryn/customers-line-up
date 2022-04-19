using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Messages.Send
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, Result>
    {
        private readonly IValidator<Domain.Messages.Message> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public SendMessageHandler(
            IValidator<Domain.Messages.Message> validator,
            ICLupDbContext context,
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(SendMessageCommand command, CancellationToken cancellationToken)
            => await _context.Users.FirstOrDefaultAsync(user => user.Id == command.SenderId)
                .ToResult()
                .AndThen(async user =>
                {
                    var business =
                        await _context.Businesses.FirstOrDefaultAsync(business => business.Id == command.SenderId);
                    return new { business, user };
                })
                .EnsureDiscard(anon => anon.user != null || anon.business != null, "Invalid sender.")
                .AndThen(async () =>
                {
                    var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == command.ReceiverId);
                    var business =
                        await _context.Businesses.FirstOrDefaultAsync(business => business.Id == command.ReceiverId);
                    return new { business, user };
                })
                .EnsureDiscard(anon => anon.user != null || anon.business != null, "Invalid receiver.")
                .AndThen(() => _mapper.Map<Domain.Messages.Message>(command))
                .Validate(_validator)
                .Finally(message => _context.AddAndSave(message));
    }
}