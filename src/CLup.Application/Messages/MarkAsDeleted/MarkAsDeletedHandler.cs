using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Shared.Messages.MarkAsDeleted
{
    public class MarkAsDeletedHandler : IRequestHandler<MarkMessageAsDeletedCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public MarkAsDeletedHandler(ICLupDbContext context) => _context = context;
  

        public async Task<Result> Handle(MarkMessageAsDeletedCommand command, CancellationToken cancellationToken)
        {
            return await _context.Messages.Include(message => message.Metadata).FirstOrDefaultAsync(message => message.Id == command.MessageId)
                .ToResult()
                .AndThen(message => command.ForSender ? message.DeletedBySender() : message.DeletedByReceiver())
                .Finally(message => _context.UpdateEntity(message.Id, message));
        }
    }
}