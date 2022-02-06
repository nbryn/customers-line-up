using System.Threading;
using System.Threading.Tasks;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Shared.Interfaces
{
    public interface ICLupDbContext
    {
        DbSet<Booking> Bookings { get; }
        
        DbSet<BusinessOwner> BusinessOwners { get; }
        
        DbSet<Business> Businesses { get; }
        
        DbSet<Employee> Employees { get; }
        
        DbSet<TimeSlot> TimeSlots { get; }
        
        DbSet<User> Users { get; }
        
        DbSet<Message> Messages { get; }

        Task<int> AddAndSave(params Entity[] entities);
        
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);

        void CreateEntityIfNotExists<T>(T existingEntity, T newEntity) where T : Entity;

        Task<int> RemoveAndSave(Entity value);
        
        Task<int> UpdateEntity<T>(string id, T updatedEntity) where T : Entity;
    }
}