using System;
using CLup.Domain.Businesses;
using CLup.Domain.Messages;
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
                a.Property(a => a.Street)
                    .HasColumnName("Street");

                a.Property(a => a.Zip)
                    .HasColumnName("Zip");

                a.Property(a => a.City)
                    .HasColumnName("City");
            });

            businessConfiguration.OwnsOne(b => b.BusinessData, b =>
            {
                b.Property(b => b.Name)
                    .HasColumnName("Name");

                b.Property(b => b.Capacity)
                    .HasColumnName("Capacity");

                b.Property(b => b.TimeSlotLength)
                    .HasColumnName("TimeSlotLength");
            });

            businessConfiguration.OwnsOne(b => b.Coords, c =>
            {
                c.Property(c => c.Latitude)
                    .HasColumnName("Latitude");

                c.Property(c => c.Longitude)
                    .HasColumnName("Longitude");
            });

            businessConfiguration.OwnsOne(b => b.BusinessHours, t =>
            {
                t.Property(t => t.Start)
                    .HasColumnName("Start");

                t.Property(t => t.End)
                    .HasColumnName("End");
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

            businessConfiguration
                    .HasMany<Message>(business => business.SentMessages)
                    .WithOne()
                    .HasForeignKey(message => message.SenderId);

            businessConfiguration
                    .HasMany<Message>(business => business.ReceivedMessages)
                    .WithOne()
                    .HasForeignKey(message => message.ReceiverId);
        }
    }
}