﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLup.Application.Users;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Users;

namespace CLup.Application.Shared.Interfaces
{
    public interface IReadOnlyDbContext
    {
        Task<Result<UsersNotEmployedByBusiness>> FetchUsersNotEmployedByBusiness(string businessId);
        
        public Task<User> FetchUserAggregate(string userEmail);

        public Task<IList<Business>> FetchAllBusinesses();

        public IQueryable<User> Users { get; }
    }
}