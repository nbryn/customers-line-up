using CLup.Domain.Business;
using CLup.Domain.Businesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class BusinessTypeConfiguration : IEntityTypeConfiguration<BusinessOwner>
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