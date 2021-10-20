using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain;

namespace CLup.Data.EntityConfigurations
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            userConfiguration.ToTable("users", CLupContext.DEFAULT_SCHEMA);
            userConfiguration.HasKey(b => b.Id);

            userConfiguration.OwnsOne(b => b.Address, b =>
            {
                b.Property<int>("UserId");
            });

            userConfiguration.OwnsOne(b => b.UserData, b =>
            {
                b.Property<int>("UserId");
            });

            userConfiguration.OwnsOne(b => b.Coords, b =>
            {
                b.Property<int>("UserId");
            });

            userConfiguration
                        .HasMany(x => x.Bookings)
                        .WithOne(x => x.User);
        }
    }
}