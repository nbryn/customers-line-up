using CLup.Domain.Messages;
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
            userConfiguration.HasKey(b => b.Id);

            userConfiguration.OwnsOne(b => b.Address, a =>
            {
                a.Property(a => a.Street)
                    .HasColumnName("Street");

                a.Property(p => p.Zip)
                    .HasColumnName("Zip");

                a.Property(a => a.City)
                    .HasColumnName("City");
            });

            userConfiguration.OwnsOne(b => b.UserData, u =>
            {
                u.Property(u => u.Name)
                   .HasColumnName("Name");


                u.Property(u => u.Email)
                    .HasColumnName("Email");

                u.Property(u => u.Password)
                    .HasColumnName("Password");
            });

            userConfiguration.OwnsOne(b => b.Coords, c =>
            {
                c.Property(c => c.Latitude)
                    .HasColumnName("Latitude");
                    
                c.Property(c => c.Longitude)
                    .HasColumnName("Longitude");
            });

            userConfiguration
                    .HasMany(x => x.Bookings)
                    .WithOne(x => x.User);
            
             userConfiguration
                    .HasMany(x => x.Businesses)
                    .WithOne(x => x.Owner);

           userConfiguration
                    .HasMany<Message>(user => user.SentMessages)
                    .WithOne()
                    .HasForeignKey(message => message.SenderId);

            userConfiguration
                    .HasMany<Message>(user => user.ReceivedMessages)
                    .WithOne()
                    .HasForeignKey(message => message.ReceiverId);
        }
    }
}