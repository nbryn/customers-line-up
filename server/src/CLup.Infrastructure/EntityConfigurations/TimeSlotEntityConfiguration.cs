using CLup.Domain.Business.TimeSlot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.EntityConfigurations
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