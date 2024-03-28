using CLup.Domain.Employees;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Infrastructure.Persistence.EntityConfigurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
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

        builder.OwnsOne(user => user.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street");

            address.Property(a => a.Zip)
                .HasColumnName("Zip");

            address.Property(a => a.City)
                .HasColumnName("City");

            address.OwnsOne(a => a.Coords, coords =>
            {
                coords.Property(c => c.Latitude)
                    .HasColumnName("Latitude");

                coords.Property(c => c.Longitude)
                    .HasColumnName("Longitude");
            });
        });

        builder.OwnsOne(user => user.UserData, u =>
        {
            u.Property(userData => userData.Name)
                .HasColumnName("Name");

            u.Property(userData => userData.Email)
                .HasColumnName("Email");

            u.Property(userData => userData.Password)
                .HasColumnName("Password");
        });

        builder.HasMany(user => user.Businesses)
            .WithOne()
            .HasForeignKey(business => business.OwnerId)
            .IsRequired();

        builder.HasMany(user => user.Bookings)
            .WithOne(booking => booking.User)
            .HasForeignKey(booking => booking.UserId)
            .IsRequired();


        builder.HasMany(user => user.SentMessages)
            .WithOne()
            .HasForeignKey(message => message.SenderId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasMany(user => user.ReceivedMessages)
            .WithOne()
            .HasForeignKey(message => message.ReceiverId)
            .IsRequired();

        builder.HasOne<Employee>()
            .WithOne(employee => employee.User)
            .HasForeignKey<Employee>(employee => employee.UserId)
            .IsRequired();
    }
}
