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
            businessConfiguration.ToTable("businesses", CLupContext.DEFAULT_SCHEMA);
            businessConfiguration.HasKey(b => b.Id);

            businessConfiguration.OwnsOne(b => b.Address, b =>
            {
                b.Property<int>("BusinessId");
            });

            businessConfiguration.OwnsOne(b => b.BusinessData, b =>
            {
                b.Property<int>("BusinessId");
            });

            businessConfiguration.OwnsOne(b => b.Coords, b =>
            {
                b.Property<int>("BusinessId");
            });

            businessConfiguration.OwnsOne(b => b.BusinessHours, b =>
            {
                b.Property<int>("BusinessId");
            });

            businessConfiguration
                       .Property(b => b.Type)
                       .HasConversion(b => b.ToString("G"),
                       b => Enum.Parse<BusinessType>(b));

            businessConfiguration
                        .HasMany(x => x.TimeSlots)
                        .WithOne(x => x.Business);

            businessConfiguration
                        .HasMany(x => x.Employees)
                        .WithOne(x => x.Business);
        }
    }
}