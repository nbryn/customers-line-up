using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain;

namespace CLup.Data.EntityConfigurations
{
    class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> bookingConfiguration)
        {
            bookingConfiguration.ToTable("bookings", CLupContext.DEFAULT_SCHEMA);
            bookingConfiguration.HasKey(b => b.Id);
        }
    }
}