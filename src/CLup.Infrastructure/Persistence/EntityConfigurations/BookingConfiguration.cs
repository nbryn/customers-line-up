using CLup.Domain.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> bookingConfiguration)
        {
            bookingConfiguration.Ignore(b => b.DomainEvents);
            bookingConfiguration.ToTable("bookings");
            bookingConfiguration.HasKey(b => b.Id);
        }
    }
}