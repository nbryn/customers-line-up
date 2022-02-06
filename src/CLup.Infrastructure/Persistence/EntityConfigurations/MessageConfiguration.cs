using System;
using CLup.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLup.Infrastructure.Persistence.EntityConfigurations
{
    class BusinessMessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> messageConfiguration)
        {
            messageConfiguration.ToTable("Messages");
            messageConfiguration.HasKey(b => b.Id);

            messageConfiguration
                    .Property(b => b.Type)
                    .HasConversion(b => b.ToString("G"), b => Enum.Parse<MessageType>(b));

            messageConfiguration.OwnsOne(m => m.MessageData, md =>
            {
                md.Property(md => md.Title)
                    .HasColumnName("Title");

                md.Property(md => md.Content)
                    .HasColumnName("Content");
            });

            messageConfiguration.OwnsOne(m => m.Metadata, metaData =>
           {
               metaData.Property(md => md.DeletedBySender)
                   .HasColumnName("DeletedBySender");

               metaData.Property(md => md.DeletedByReceiver)
                   .HasColumnName("DeletedByReceiver");
           });
        }
    }
}