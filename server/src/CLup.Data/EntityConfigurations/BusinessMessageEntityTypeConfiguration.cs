using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain.Businesses;
using CLup.Domain.Messages;

namespace CLup.Data.EntityConfigurations
{
    class BusinessMessageEntityTypeConfiguration : IEntityTypeConfiguration<BusinessMessage>
    {
        public void Configure(EntityTypeBuilder<BusinessMessage> businessMessageConfiguration)
        {
            businessMessageConfiguration.ToTable("businessMessages");
            businessMessageConfiguration.HasKey(b => b.Id);

            businessMessageConfiguration
                    .Property(b => b.Type)
                    .HasConversion(b => b.ToString("G"), b => Enum.Parse<MessageType>(b));

            businessMessageConfiguration.OwnsOne(m => m.MessageData, md =>
            {
                md.Property(md => md.Title)
                    .HasColumnName("Title");

                md.Property(md => md.Content)
                    .HasColumnName("Content");
            });

            businessMessageConfiguration
                .HasOne(m => m.Sender)
                .WithMany(b => b.SentMessages);

            businessMessageConfiguration
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages);
        }
    }
}