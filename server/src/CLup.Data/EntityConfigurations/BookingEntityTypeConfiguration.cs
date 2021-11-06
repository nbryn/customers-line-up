using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain.Bookings;

namespace CLup.Data.EntityConfigurations
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