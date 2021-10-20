using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Employees.Commands
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(CLupContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == command.UserId)
                    .FailureIfDiscard("User not found.")
                    .FailureIfDiscard(() => _context.Businesses.FirstOrDefaultAsync(b => b.Id == command.BusinessId), "Business not found.")
                    .AndThen(() => _mapper.Map<Employee>(command))
                    .Finally(employee => _context.AddAndSave(employee));
        }
    }
}