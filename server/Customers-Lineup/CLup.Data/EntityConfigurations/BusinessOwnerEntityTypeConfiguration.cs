using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain;

namespace CLup.Data.EntityConfigurations
{
    class BusinessOwnerEntityTypeConfiguration : IEntityTypeConfiguration<BusinessOwner>
    {
        public void Configure(EntityTypeBuilder<BusinessOwner> businessOwnerConfiguration)
        {
            businessOwnerConfiguration.ToTable("businessowners");
            businessOwnerConfiguration.HasKey(b => b.Id);
            
            businessOwnerConfiguration.HasMany(c => c.Businesses);
            businessOwnerConfiguration
                    .HasIndex(bo => bo.UserEmail)
                    .IsUnique();
        }
    }
}