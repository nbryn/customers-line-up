using CLup.Domain.Bookings;
using CLup.Domain.TimeSlots;
using CLup.Domain.TimeSlots.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.ToTable("timeslots");
        builder.Ignore(timeSlot => timeSlot.DomainEvents);
        builder.HasKey(timeSlot => timeSlot.Id);
        builder.Property(timeSlot => timeSlot.Id)
            .ValueGeneratedNever()
            .HasConversion(
            timeSlotId => timeSlotId.Value,
            value => TimeSlotId.Create(value));

        builder.HasMany<Booking>()
            .WithOne()
            .HasForeignKey(booking => booking.TimeSlotId)
            .IsRequired();
    }
}