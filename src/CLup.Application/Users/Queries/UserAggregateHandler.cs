using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users.Queries
{
    public class UserAggregateHandler : IRequestHandler<UserAggregateQuery, Result<UserDto>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public UserAggregateHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }
        
        public async Task<Result<UserDto>> Handle(UserAggregateQuery query, CancellationToken cancellationToken)
        {
            var user = await _readOnlyContext.Users 
                .Include(user => user.SentMessages)
                .Include(user => user.ReceivedMessages)
                .Include(user => user.Bookings)
                    .ThenInclude(booking => booking.Business)
                .Include(user => user.Bookings)
                    .ThenInclude(booking => booking.TimeSlot)
                    .ThenInclude(timeSlot => timeSlot.Business)
                .Include(user => user.Businesses)
                    .ThenInclude(business => business.Bookings)
                    .ThenInclude(booking => booking.TimeSlot)
                    .ThenInclude(timeSlot => timeSlot.Business)
                .Include(user => user.Businesses)
                    .ThenInclude(business => business.Bookings)
                    .ThenInclude(booking => booking.User)
                .Include(user => user.Businesses)
                    .ThenInclude(business => business.ReceivedMessages)
                .Include(user => user.Businesses)
                    .ThenInclude(business => business.SentMessages)
                .Include(user => user.Businesses)
                    .ThenInclude(business => business.Employees)
                    .ThenInclude(employee => employee.User)
                .Include(user => user.Businesses)
                    .ThenInclude(business => business.TimeSlots)
                    .ThenInclude(timeSlot => timeSlot.Bookings)
                .AsSplitQuery()
                .FirstOrDefaultAsync(user => user.UserData.Email == query.UserEmail);
            if (user == null)
            {
                return Result.NotFound<UserDto>();
            }

            return Result.Ok<UserDto>(_mapper.Map<UserDto>(user));
        }
    }
}