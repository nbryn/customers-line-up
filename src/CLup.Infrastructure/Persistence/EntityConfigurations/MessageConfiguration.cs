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
            messageConfiguration.ToTable("messages");
            messageConfiguration.HasKey(message => message.Id);

            messageConfiguration
                    .Property(message => message.Type)
                    .HasConversion(type => type.ToString("G"), type => Enum.Parse<MessageType>(type));

            messageConfiguration.OwnsOne(message => message.MessageData, md =>
            {
                md.Property(messageData => messageData.Title)
                    .HasColumnName("Title");

                md.Property(messageData => messageData.Content)
                    .HasColumnName("Content");
            });

            messageConfiguration.OwnsOne(message => message.Metadata, metaData =>
           {
               metaData.Property(messageMetadata => messageMetadata.DeletedBySender)
                   .HasColumnName("DeletedBySender");

               metaData.Property(messageMetadata => messageMetadata.DeletedByReceiver)
                   .HasColumnName("DeletedByReceiver");
           });
        }
    }
}