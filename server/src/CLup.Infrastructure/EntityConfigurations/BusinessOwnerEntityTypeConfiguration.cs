using CLup.Domain.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.EntityConfigurations
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