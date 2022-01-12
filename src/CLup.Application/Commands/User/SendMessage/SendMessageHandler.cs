using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.User;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.User.SendMessage
{
    public class SendMessageHandler : IRequestHandler<SendUserMessageCommand, Result>
    {
        private readonly IValidator<UserMessage> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public SendMessageHandler (
            IValidator<UserMessage> validator,
            ICLupDbContext context, 
            IMapper mapper) 
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(SendUserMessageCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == command.SenderId)
                    .ToResult()
                    .EnsureDiscard(user => user != null, "Invalid sender.")
                    .AndThen(() => _context.Businesses.Where(business => business.Id == command.ReceiverId))
                    .EnsureDiscard(Business => Business != null, "Invalid receiver.")
                    .AndThen(() => _mapper.Map<UserMessage>(command))
                    .Validate(_validator)
                    .Finally(message => _context.AddAndSave(message));
        }
    }
}