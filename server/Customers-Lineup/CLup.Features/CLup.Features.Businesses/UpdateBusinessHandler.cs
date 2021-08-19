using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Businesses.Commands;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Businesses
{
    public class UpdateBusinessHandler : IRequestHandler<UpdateBusinessCommand, Result>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UpdateBusinessHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
        {
            return await _context.Businesses.FirstOrDefaultAsync(x => x.Id == command.Id)
                    .FailureIfDiscard("Business not found.")
                    .AndThen(() => _mapper.Map<Business>(command))
                    .Finally(updatedBusiness => _context.UpdateEntity(command.Id, updatedBusiness));
        }
    }
}