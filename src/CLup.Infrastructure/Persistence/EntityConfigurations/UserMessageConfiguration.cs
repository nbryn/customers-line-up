using System;
using CLup.Domain.Message;
using CLup.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class UserMessageConfiguration : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> userMessageConfiguration)
        {
            userMessageConfiguration.ToTable("userMessages");
            userMessageConfiguration.HasKey(b => b.Id);

            userMessageConfiguration.OwnsOne(m => m.MessageData, md =>
            {
                md.Property(md => md.Title)
                    .HasColumnName("Title");

                md.Property(md => md.Content)
                    .HasColumnName("Content");
            });

            userMessageConfiguration.OwnsOne(m => m.Metadata, metaData =>
           {
               metaData.Property(md => md.DeletedBySender)
                   .HasColumnName("DeletedBySender");

               metaData.Property(md => md.DeletedByReceiver)
                   .HasColumnName("DeletedByReceiver");
           });

            userMessageConfiguration
                    .Property(b => b.Type)
                    .HasConversion(b => b.ToString("G"), b => Enum.Parse<MessageType>(b));

            userMessageConfiguration
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages);

            userMessageConfiguration
                .HasOne(m => m.Receiver)
                .WithMany(b => b.ReceivedMessages);
        }
    }
}