using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain.Businesses;
using CLup.Domain.Messages;
using CLup.Domain.Users;

namespace CLup.Data.EntityConfigurations
{
    class BusinessMessageEntityTypeConfiguration : IEntityTypeConfiguration<Message<Business, User>>
    {
        public void Configure(EntityTypeBuilder<Message<Business, User>> businessMessageConfiguration)
        {
            businessMessageConfiguration.ToTable("businessMessages");
            businessMessageConfiguration.HasKey(b => b.Id);

            businessMessageConfiguration
                    .Property(b => b.Type)
                    .HasConversion(b => b.ToString("G"), b => Enum.Parse<MessageType>(b));

            businessMessageConfiguration
                .HasOne(m => m.Sender)
                .WithMany(b => b.SentMessages);
            
            businessMessageConfiguration
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages);
        }
    }
}