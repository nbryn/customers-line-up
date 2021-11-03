using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CLup.Domain.Businesses;
using CLup.Domain.Messages;
using CLup.Domain.Users;

namespace CLup.Data.EntityConfigurations
{
    class UserMessageEntityTypeConfiguration : IEntityTypeConfiguration<Message<User, Business>>
    {
        public void Configure(EntityTypeBuilder<Message<User, Business>> userMessageConfiguration)
        {
            userMessageConfiguration.ToTable("userMessages");
            userMessageConfiguration.HasKey(b => b.Id);

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