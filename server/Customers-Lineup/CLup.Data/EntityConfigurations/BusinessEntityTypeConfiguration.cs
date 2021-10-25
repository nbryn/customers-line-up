using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain;

namespace CLup.Data.EntityConfigurations
{
    class BusinessEntityTypeConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> businessConfiguration)
        {
            businessConfiguration.ToTable("businesses");
            businessConfiguration.HasKey(b => b.Id);

            businessConfiguration.OwnsOne(b => b.Address, a =>
            {
                a.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasDefaultValue("");

                a.Property(a => a.Zip)
                    .HasColumnName("Zip")
                    .HasDefaultValue("");

                a.Property(a => a.City)
                    .HasColumnName("City")
                    .HasDefaultValue("");
            });

            businessConfiguration.OwnsOne(b => b.BusinessData, b =>
            {
                b.Property(b => b.Name)
                    .HasColumnName("Name")
                    .HasDefaultValue("");

                b.Property(b => b.Capacity)
                    .HasColumnName("Capacity")
                .HasDefaultValue(0);

                b.Property(b => b.TimeSlotLength)
                    .HasColumnName("TimeSlotLength")
                    .HasDefaultValue(0);
            });

            businessConfiguration.OwnsOne(b => b.Coords, c =>
            {
                c.Property(c => c.Latitude)
                    .HasColumnName("Latitude")
                    .HasDefaultValue(0.00);

                c.Property(c => c.Longitude)
                    .HasColumnName("Longitude")
                    .HasDefaultValue(0.00);
            });

            businessConfiguration.OwnsOne(b => b.BusinessHours, t =>
            {
                t.Property(t => t.Start)
                    .HasColumnName("Start")
                .HasDefaultValue("");

                t.Property(t => t.End)
                    .HasColumnName("End")
                    .HasDefaultValue("");
            });

            businessConfiguration
                    .Property(b => b.Type)
                    .HasConversion(b => b.ToString("G"), b => Enum.Parse<BusinessType>(b));

            businessConfiguration
                    .HasMany(x => x.TimeSlots)
                    .WithOne(x => x.Business);

            businessConfiguration
                    .HasMany(x => x.Employees)
                    .WithOne(x => x.Business);
        }
    }
}