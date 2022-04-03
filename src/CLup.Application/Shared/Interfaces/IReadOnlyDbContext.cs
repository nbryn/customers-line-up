using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Users;

namespace CLup.Application.Shared.Interfaces
{
    public interface IReadOnlyDbContext
    {
        public Task<User> FetchUserAggregate(string userEmail);

        public Task<IList<Business>> FetchAllBusinesses();

        public IQueryable<Booking> Bookings { get; }
        
        public IQueryable<Business> Businesses { get; }
        
        public IQueryable<Employee> Employees { get; }
        
        public IQueryable<User> Users { get; }
    }
}