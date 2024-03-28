using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal sealed class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable("businesses");
        builder.HasKey(business => business.Id);

        builder.Property(business => business.Id)
            .ValueGeneratedNever()
            .HasConversion(
            businessId => businessId.Value,
            value => BusinessId.Create(value));

        builder.OwnsOne(business => business.Address, a =>
        {
            a.Property(address => address.Street)
                .HasColumnName("Street");

            a.Property(address => address.Zip)
                .HasColumnName("Zip");

            a.Property(address => address.City)
                .HasColumnName("City");

            a.OwnsOne(address => address.Coords, coords =>
            {
                coords.Property(c => c.Latitude)
                    .HasColumnName("Latitude");

                coords.Property(c => c.Longitude)
                    .HasColumnName("Longitude");
            });
        });

        builder.OwnsOne(business => business.BusinessData, b =>
        {
            b.Property(businessData => businessData.Name)
                .HasColumnName("Name");

            b.Property(businessData => businessData.Capacity)
                .HasColumnName("Capacity");

            b.Property(businessData => businessData.TimeSlotLengthInMinutes)
                .HasColumnName("TimeSlotLength");
        });

        builder.OwnsOne(business => business.BusinessHours, t =>
        {
            t.Property(timeSpan => timeSpan.Start)
                .HasColumnName("Start");

            t.Property(timeSpan => timeSpan.End)
                .HasColumnName("End");
        });

        builder.Property(business => business.Type)
            .HasConversion(businessType => businessType.ToString("G"), b => Enum.Parse<BusinessType>(b));

        builder.HasMany(business => business.TimeSlots)
            .WithOne(timeSlot => timeSlot.Business)
            .HasForeignKey(timeSlot => timeSlot.BusinessId)
            .IsRequired();

        builder.HasMany(business => business.Bookings)
            .WithOne(booking => booking.Business)
            .HasForeignKey(booking => booking.BusinessId)
            .IsRequired();

        builder.HasMany(business => business.Employees)
            .WithOne(employee => employee.Business)
            .HasForeignKey(employee => employee.BusinessId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasMany(business => business.SentMessages)
            .WithOne()
            .HasForeignKey(message => message.SenderId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasMany(business => business.ReceivedMessages)
            .WithOne()
            .HasForeignKey(message => message.ReceiverId)
            .IsRequired();
    }
}
