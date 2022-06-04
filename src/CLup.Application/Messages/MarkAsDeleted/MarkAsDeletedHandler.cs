using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Messages.MarkAsDeleted
{
    public class MarkAsDeletedHandler : IRequestHandler<MarkMessageAsDeletedCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public MarkAsDeletedHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(MarkMessageAsDeletedCommand command, CancellationToken cancellationToken)
            => await _context.Messages
                .Include(message => message.Metadata).FirstOrDefaultAsync(message => message.Id == command.MessageId)
                .FailureIf("Message not found.")
                .AndThen(message => command.ForSender ? message.DeletedBySender() : message.DeletedByReceiver())
                .Finally(message => _context.UpdateEntity(message.Id, message));
    }
}