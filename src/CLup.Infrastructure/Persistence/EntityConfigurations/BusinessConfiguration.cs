using System;
using CLup.Domain.Businesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> businessConfiguration)
        {
            businessConfiguration.ToTable("businesses");
            businessConfiguration.HasKey(b => b.Id);

            businessConfiguration.OwnsOne(b => b.Address, a =>
            {
                a.Property(address => address.Street)
                    .HasColumnName("Street");

                a.Property(a => a.Zip)
                    .HasColumnName("Zip");

                a.Property(address => address.City)
                    .HasColumnName("City");
            });

            businessConfiguration.OwnsOne(b => b.BusinessData, b =>
            {
                b.Property(businessData => businessData.Name)
                    .HasColumnName("Name");

                b.Property(businessData => businessData.Capacity)
                    .HasColumnName("Capacity");

                b.Property(businessData => businessData.TimeSlotLength)
                    .HasColumnName("TimeSlotLength");
            });

            businessConfiguration.OwnsOne(b => b.Coords, c =>
            {
                c.Property(coords => coords.Latitude)
                    .HasColumnName("Latitude");

                c.Property(coords => coords.Longitude)
                    .HasColumnName("Longitude");
            });

            businessConfiguration.OwnsOne(b => b.BusinessHours, t =>
            {
                t.Property(timeSpan => timeSpan.Start)
                    .HasColumnName("Start");

                t.Property(timeSpan => timeSpan.End)
                    .HasColumnName("End");
            });

            businessConfiguration
                    .Property(business => business.Type)
                    .HasConversion(b => b.ToString("G"), b => Enum.Parse<BusinessType>(b));

            businessConfiguration
                    .HasMany(business => business.TimeSlots)
                    .WithOne(timeSlot => timeSlot.Business);

            businessConfiguration
                    .HasMany(business => business.Employees)
                    .WithOne(employee => employee.Business);

            businessConfiguration
                .HasMany(business => business.SentMessages)
                .WithOne();

            businessConfiguration
                .HasMany(business => business.ReceivedMessages)
                .WithOne();
        }
    }
}