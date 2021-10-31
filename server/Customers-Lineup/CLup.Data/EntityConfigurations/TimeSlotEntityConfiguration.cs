using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain.Businesses.TimeSlots;

namespace CLup.Data.EntityConfigurations
{
    class TimeSlotEntityTypeConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> timeSlotConfiguration)
        {
            timeSlotConfiguration.ToTable("timeslots");
            timeSlotConfiguration.HasKey(b => b.Id);

            timeSlotConfiguration
                        .HasMany(t => t.Bookings)
                        .WithOne(b => b.TimeSlot)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}