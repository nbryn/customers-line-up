using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain;

namespace CLup.Data.EntityConfigurations
{
    class TimeSlotEntityTypeConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> timeSlotConfiguration)
        {
            timeSlotConfiguration.ToTable("timeslots", CLupContext.DEFAULT_SCHEMA);
            timeSlotConfiguration.HasKey(b => b.Id);

            timeSlotConfiguration
                        .HasMany(t => t.Bookings)
                        .WithOne(b => b.TimeSlot);
        }
    }
}