using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            userConfiguration.ToTable("users");
            userConfiguration.HasKey(user => user.Id);

            userConfiguration.OwnsOne(user => user.Address, a =>
            {
                a.Property(address => address.Street)
                    .HasColumnName("Street");

                a.Property(address => address.Zip)
                    .HasColumnName("Zip");

                a.Property(address => address.City)
                    .HasColumnName("City");
            });

            userConfiguration.OwnsOne(b => b.UserData, u =>
            {
                u.Property(userData => userData.Name)
                    .HasColumnName("Name");


                u.Property(userData => userData.Email)
                    .HasColumnName("Email");

                u.Property(userData => userData.Password)
                    .HasColumnName("Password");
            });

            userConfiguration.OwnsOne(user => user.Coords, c =>
            {
                c.Property(coords => coords.Latitude)
                    .HasColumnName("Latitude");

                c.Property(coords => coords.Longitude)
                    .HasColumnName("Longitude");
            });

            userConfiguration
                .HasMany(user => user.Bookings)
                .WithOne(booking => booking.User);

            userConfiguration
                .HasMany(user => user.Businesses)
                .WithOne(business => business.Owner);

            userConfiguration
                .HasMany(user => user.SentMessages)
                .WithOne();

            userConfiguration
                .HasMany(user => user.ReceivedMessages)
                .WithOne();
        }
    }
}