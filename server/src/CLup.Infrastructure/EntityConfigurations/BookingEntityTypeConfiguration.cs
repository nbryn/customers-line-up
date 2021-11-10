using CLup.Domain.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.EntityConfigurations
{
    class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> bookingConfiguration)
        {
            bookingConfiguration.ToTable("bookings");
            bookingConfiguration.HasKey(b => b.Id);
        }
    }
}