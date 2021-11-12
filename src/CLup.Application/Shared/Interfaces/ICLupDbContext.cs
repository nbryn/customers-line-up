using System.Threading;
using System.Threading.Tasks;
using CLup.Domain.Booking;
using CLup.Domain.Business;
using CLup.Domain.Business.Employee;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.Shared;
using CLup.Domain.User;
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
        
        DbSet<BusinessMessage> BusinessMessages { get; }
        
        DbSet<UserMessage> UserMessages { get; }

        Task<int> AddAndSave(params Entity[] entities);
        
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);

        void CreateEntityIfNotExists<T>(T existingEntity, T newEntity) where T : Entity;

        Task<int> RemoveAndSave(Entity value);
        
        Task<int> UpdateEntity<T>(string id, T updatedEntity) where T : Entity;
    }
}