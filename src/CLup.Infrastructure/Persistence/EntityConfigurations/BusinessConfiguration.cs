using System;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Messages;
using CLup.Domain.TimeSlots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal class BusinessConfiguration : IEntityTypeConfiguration<Business>
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
        });

        builder.OwnsOne(business => business.BusinessData, b =>
        {
            b.Property(businessData => businessData.Name)
                .HasColumnName("Name");

            b.Property(businessData => businessData.Capacity)
                .HasColumnName("Capacity");

            b.Property(businessData => businessData.TimeSlotLength)
                .HasColumnName("TimeSlotLength");
        });

        builder.OwnsOne(business => business.Coords, c =>
        {
            c.Property(coords => coords.Latitude)
                .HasColumnName("Latitude");

            c.Property(coords => coords.Longitude)
                .HasColumnName("Longitude");
        });

        builder.OwnsOne(business => business.BusinessHours, t =>
        {
            t.Property(timeSpan => timeSpan.Start)
                .HasColumnName("Start");

            t.Property(timeSpan => timeSpan.End)
                .HasColumnName("End");
        });

        builder
            .Property(business => business.Type)
            .HasConversion(businessType => businessType.ToString("G"), b => Enum.Parse<BusinessType>(b));

        builder.HasMany<TimeSlot>()
            .WithOne()
            .HasForeignKey(timeSlot => timeSlot.BusinessId)
            .IsRequired();

        builder.HasMany<Employee>()
            .WithOne()
            .HasForeignKey(employee => employee.BusinessId)
            .IsRequired();

        builder.HasMany<Message>()
            .WithOne()
            .HasForeignKey(message => message.SenderId)
            .IsRequired();

        builder.HasMany<Message>()
            .WithOne()
            .HasForeignKey(message => message.ReceiverId)
            .IsRequired();
    }
}