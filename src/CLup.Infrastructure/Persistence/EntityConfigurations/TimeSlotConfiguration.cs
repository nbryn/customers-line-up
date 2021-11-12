using CLup.Domain.Business.TimeSlot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> timeSlotConfiguration)
        {
            timeSlotConfiguration.Ignore(t => t.DomainEvents);
            timeSlotConfiguration.ToTable("timeslots");
            timeSlotConfiguration.HasKey(b => b.Id);

            timeSlotConfiguration
                        .HasMany(t => t.Bookings)
                        .WithOne(b => b.TimeSlot)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}