using CLup.Domain.Bookings;
using CLup.Domain.Bookings.ValueObjects;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        builder.Ignore(booking => booking.DomainEvents);
        builder.HasKey(booking => booking.Id);
        builder.Property(booking => booking.Id)
            .ValueGeneratedNever()
            .HasConversion(
                bookingId => bookingId.Value,
                value => BookingId.Create(value));
    }
}
