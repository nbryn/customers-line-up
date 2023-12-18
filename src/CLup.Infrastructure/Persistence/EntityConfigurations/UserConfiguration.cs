using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Messages;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .HasConversion(
            userId => userId.Value,
            value => UserId.Create(value));

        builder.OwnsOne(user => user.Address, a =>
        {
            a.Property(address => address.Street)
                .HasColumnName("Street");

            a.Property(address => address.Zip)
                .HasColumnName("Zip");

            a.Property(address => address.City)
                .HasColumnName("City");
        });

        builder.OwnsOne(b => b.UserData, u =>
        {
            u.Property(userData => userData.Name)
                .HasColumnName("Name");


            u.Property(userData => userData.Email)
                .HasColumnName("Email");

            u.Property(userData => userData.Password)
                .HasColumnName("Password");
        });

        builder.OwnsOne(user => user.Coords, c =>
        {
            c.Property(coords => coords.Latitude)
                .HasColumnName("Latitude");

            c.Property(coords => coords.Longitude)
                .HasColumnName("Longitude");
        });

        builder.HasMany<Booking>()
            .WithOne()
            .HasForeignKey(booking => booking.UserId)
            .IsRequired();

        builder.HasMany<Business>()
            .WithOne()
            .HasForeignKey(business => business.OwnerId)
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